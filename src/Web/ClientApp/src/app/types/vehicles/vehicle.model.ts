import { ColourId } from "../colours/colourId.enum";
export interface Vehicle {
  vehicleId: Number,
  manufacturerName: string,
  model: string,
  modelYear: number,
  colourId?: ColourId | null
}
