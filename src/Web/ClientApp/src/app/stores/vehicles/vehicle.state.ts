import { VehicleStateModel } from "./vehicle-state.model";
import { Action, Selector, State, StateContext, StateToken } from "@ngxs/store";
import { Injectable } from "@angular/core";
import { VehicleActions } from "./vehicle.actions";
import { VehiclesClientService } from "../../services/vehicles-client.service";
import { of } from "rxjs";
import { catchError, tap } from "rxjs/operators";
import produce from "immer";
import { VehicleListItemModel } from "../../types/vehicles/vehicle-list-item.model";
import { PaginatedListModel } from 'src/app/types/paginated-list.model';
import { ApiResponse } from "src/app/types/apiResponse.model";
import { Vehicle } from "src/app/types/vehicles/vehicle.model";
import { VehicleForm } from "src/app/types/vehicles/vehicleForm.model";

const VEHICLE_STATE_TOKEN = new StateToken<VehicleStateModel>('vehicle');

@State<VehicleStateModel>({
  name: VEHICLE_STATE_TOKEN,
  defaults: {
    vehicleListItems: {
      items: [],
      pageNumber: 1,
      totalPages: 1,
      totalCount: 0
    },
    isLoading: false,
    pageSize: 10,
    responseMessage: null,
    isLoadingSingle: false,
    selectedVehicle: {
      vehicleId: 0,
      manufacturerName: '',
      model: '',
      modelYear: 0,
      colourId: null
    }
  }
})
@Injectable()
export class VehicleState {
  constructor(private _vehiclesClient: VehiclesClientService) { }

  @Selector()
  static getVehicleListItems(state: VehicleStateModel): VehicleListItemModel[] {
    return state.vehicleListItems.items;
  }

  @Selector()
  static getLoading(state: VehicleStateModel): boolean {
    return state.isLoading;
  }

  @Selector()
  static getLoadingSingle(state: VehicleStateModel): boolean {
    return state.isLoadingSingle;
  }

  @Selector()
  static getPageSize(state: VehicleStateModel): number {
    return state.pageSize;
  }

  @Selector()
  static getPagination(state: VehicleStateModel): { pageNumber: number, totalPages: number, totalCount: number } {
    return {
      pageNumber: state.vehicleListItems.pageNumber,
      totalPages: state.vehicleListItems.totalPages,
      totalCount: state.vehicleListItems.totalCount
    };
  }

  @Selector()
  static getResponseMessage(state: VehicleStateModel): string | null {
    return state.responseMessage;
  }

  @Action(VehicleActions.LoadVehicles)
  loadVehicles(ctx: StateContext<VehicleStateModel>, action: VehicleActions.LoadVehicles) {
    const { pageNumber, pageSize } = action;
    const state = ctx.getState();

    if (state.isLoading && state.vehicleListItems.pageNumber === pageNumber) {
      return of(null);
    }

    ctx.patchState({ isLoading: true, responseMessage: null });

    return this._vehiclesClient.getVehiclesPaginated(pageNumber, pageSize).pipe(
      tap((paginatedList: PaginatedListModel<VehicleListItemModel>) => {
        ctx.setState(produce(ctx.getState(), draft => {
          draft.vehicleListItems = paginatedList;
          draft.isLoading = false;
        }));
      }),
      catchError((error) => {
        console.error('Error loading vehicles');
        ctx.patchState({ isLoading: false, responseMessage: 'Failed to load vehicles' });
        return of(null);
      })
    );
  }

  @Action(VehicleActions.SetPagination)
  setPagination(ctx: StateContext<VehicleStateModel>, action: VehicleActions.SetPagination) {
    ctx.setState(produce(ctx.getState(), draft => {
      draft.vehicleListItems.pageNumber = action.pageNumber;
      draft.vehicleListItems.totalPages = action.totalPages;
      draft.vehicleListItems.totalCount = action.totalCount;
      draft.pageSize = action.pageSize;
    }));
  }

  @Action(VehicleActions.SetPageSize)
  setPageSize(ctx: StateContext<VehicleStateModel>, action: VehicleActions.SetPageSize) {
    ctx.patchState({
      pageSize: action.pageSize
    });
  }

  @Action(VehicleActions.CreateVehicle)
  createVehicle(ctx: StateContext<VehicleStateModel>, action: VehicleActions.CreateVehicle) {
    const state = ctx.getState();
    const vehicleForm: VehicleForm = action.vehicle;
    ctx.patchState({ isLoading: true, responseMessage: null });
    return this._vehiclesClient.createVehicle(
      vehicleForm
    ).pipe(
      tap((response: ApiResponse) => {
        ctx.patchState({ isLoading: false });

        if (response.succeeded) {
          ctx.patchState({
            responseMessage: 'Success: Vehicle Created!',
          });
          ctx.dispatch(new VehicleActions.LoadVehicles(state.vehicleListItems.pageNumber, state.pageSize));
        } else {
          console.log(response.errors)
          ctx.patchState({
            responseMessage: 'Error: An error occurred during vehicle creation',
          });
        }
      }),
      catchError((error) => {
        console.error('Error creating vehicle:', error);
        ctx.patchState({ isLoading: false, responseMessage: 'Error: Failed to create vehicle' });
        return of(null);
      })
    );
  }

  @Action(VehicleActions.DeleteVehicle)
  deleteVehicle(ctx: StateContext<VehicleStateModel>, action: VehicleActions.DeleteVehicle) {
    const state = ctx.getState();
    ctx.patchState({ isLoading: true, responseMessage: null });
    console.log(action.vehicleId)
    return this._vehiclesClient.deleteVehicle(action.vehicleId).pipe(
      tap((response: ApiResponse) => {
        ctx.patchState({ isLoading: false });
        if (response.succeeded) {
          ctx.patchState({
            responseMessage: 'Success: Vehicle Deleted!',
          });
          ctx.dispatch(new VehicleActions.LoadVehicles(state.vehicleListItems.pageNumber, state.pageSize));
        } else {
          ctx.patchState({
            responseMessage: 'Error: Failed to delete vehicle',
          });
        }
      }),
      catchError((error) => {
        console.error('Error deleting vehicle:', error);
        ctx.patchState({ isLoading: false, responseMessage: 'Error: Failed to delete vehicle' });
        return of(null);
      })
    );
  }

  @Action(VehicleActions.LoadVehicleById)
  loadVehicleById(ctx: StateContext<VehicleStateModel>, action: VehicleActions.LoadVehicleById) {
    ctx.patchState({ isLoadingSingle: true, responseMessage: null });

    return this._vehiclesClient.getVehicleById(action.vehicleId).pipe(
      tap((selectedVehicle: Vehicle) => {
        ctx.setState(produce(ctx.getState(), draft => {
          draft.selectedVehicle = selectedVehicle;
          draft.isLoadingSingle = false;
        }));
      }),
      catchError((error) => {
        console.error('Error loading vehicle by ID:', error);
        ctx.patchState({
          isLoadingSingle: false,
          responseMessage: 'Failed to load vehicle details'
        });
        return of(null);
      })
    );
  }

  @Action(VehicleActions.UpdateVehicle)
  updateVehicle(ctx: StateContext<VehicleStateModel>, action: VehicleActions.UpdateVehicle) {
    const state = ctx.getState();
    if (state.isLoadingSingle || !state.selectedVehicle.vehicleId) {
      ctx.patchState({
        responseMessage: 'Error: No vehicle loaded or still loading!',
      });
      return of(null);
    }
    ctx.patchState({ isLoading: true, responseMessage: null });
    const vehicleForm: VehicleForm = action.vehicleData;
    return this._vehiclesClient.updateVehicle(state.selectedVehicle.vehicleId, vehicleForm).pipe(
      tap((response: ApiResponse) => {
        ctx.patchState({ isLoading: false });

        if (response.succeeded) {
          ctx.patchState({
            responseMessage: 'Success: Vehicle Updated!',
          });
          ctx.dispatch(new VehicleActions.LoadVehicles(state.vehicleListItems.pageNumber, state.pageSize));
        } else {
          ctx.patchState({
            responseMessage: 'Error: Failed to update vehicle',
          });
        }
      }),
      catchError((error) => {
        console.error('Error updating vehicle:', error);
        ctx.patchState({ isLoading: false, responseMessage: 'Error: Failed to update vehicle' });
        return of(null);
      })
    );
  }
}
