import { Colour } from 'src/app/types/colours/colours.model';

export interface ColourStateModel {
  items: Colour[];
  isLoading: boolean;
}
