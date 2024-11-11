import { ColourId } from "../colours/colourId.enum";
export interface VehicleForm {
  manufacturerName: string,
  model: string,
  modelYear: number,
  colourId?: ColourId
}
