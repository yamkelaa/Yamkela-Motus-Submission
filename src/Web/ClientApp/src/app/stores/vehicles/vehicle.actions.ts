import { ColourId } from "src/app/types/colours/colourId.enum";

export namespace VehicleActions {
  export class LoadVehicles {
    static readonly type = '[Vehicle] Load Vehicles';
    constructor(public pageNumber: number, public pageSize: number) { }
  }

  export class SetPagination {
    static readonly type = '[Vehicle] Set Pagination';
    constructor(public pageNumber: number, public totalPages: number, public totalCount: number, public pageSize: number) { }
  }

  export class SetPageSize {
    static readonly type = '[Vehicle] Set Page Size';
    constructor(public pageSize: number) { }
  }

  export class CreateVehicle {
    static readonly type = '[Vehicle] Create Vehicle';
    constructor(public vehicle: { manufacturerName: string, model: string, modelYear: number, colourId: ColourId }) { }
  }

  export class UpdateVehicle {
    static readonly type = '[Vehicle] Update Vehicle';
    constructor(
      public vehicleId: number,
      public vehicleData: { manufacturerName: string, model: string, modelYear: number, colourId: ColourId }
    ) { }
  }

  export class DeleteVehicle {
    static readonly type = '[Vehicle] Delete Vehicle';
    constructor(public vehicleId: number) { }
  }

  export class LoadVehicleById {
    static readonly type = '[Vehicle] Load Vehicle By ID';
    constructor(public vehicleId: number) { }
  }
}
