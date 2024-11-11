import { Colour } from "../colours/colours.model";

export interface VehicleListItemModel {
  vehicleId: number;
  manufacturerName?: string;
  model?: string;
  modelYear?: number
  colour: Colour | null;
}
