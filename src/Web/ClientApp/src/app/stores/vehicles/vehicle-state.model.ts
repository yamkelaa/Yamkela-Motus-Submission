import { PaginatedListModel } from 'src/app/types/paginated-list.model';
import { VehicleListItemModel } from 'src/app/types/vehicles/vehicle-list-item.model';
import { Vehicle } from 'src/app/types/vehicles/vehicle.model';

export interface VehicleStateModel {
  vehicleListItems: PaginatedListModel<VehicleListItemModel>;
  selectedVehicle: Vehicle | null;
  isLoading: boolean;
  isLoadingSingle: boolean;
  pageSize: number;
  responseMessage: string;
}
