import { ColourStateModel } from "./colour-state.model";
import { Action, Selector, State, StateContext, StateToken } from "@ngxs/store";
import { Injectable } from "@angular/core";
import { ColourActions } from "./colour.actions";
import { ColoursClientService } from "src/app/services/colour-client.service";
import { of, tap, catchError } from "rxjs";
import produce from "immer";
import { Colour } from "../../types/colours/colours.model";


const COLOUR_STATE_TOKEN = new StateToken<ColourStateModel>('colour');

@State<ColourStateModel>({
  name: COLOUR_STATE_TOKEN,
  defaults: {
    items: [],
    isLoading: false
  }
})
@Injectable()
export class ColourState {
  constructor(private _colourClient: ColoursClientService) { }


  @Selector()
  static getColours(state: ColourStateModel): Colour[] {
    return state.items ?? [];
  }

  @Selector()
  static getIsLoading(state: ColourStateModel): boolean {
    return state.isLoading ?? false;
  }


  @Action(ColourActions.LoadColours)
  loadColours(ctx: StateContext<ColourStateModel>, action: ColourActions.LoadColours) {
    const state = ctx.getState();
    if (state.isLoading || state.items.length > 0) {
      return of(null);
    }
    ctx.patchState({ isLoading: true });
    return this._colourClient.getVehicleColours().pipe(
      tap((colours: Colour[]) => {
        ctx.setState(produce(ctx.getState(), draft => {
          draft.items = colours;
          draft.isLoading = false;
        }));
      }),
      catchError((error) => {
        ctx.patchState({ isLoading: false });
        console.error('Error loading colours:', error);
        return of(null);
      })
    );
  }
}
