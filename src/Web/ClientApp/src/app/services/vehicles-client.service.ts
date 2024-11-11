import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { VehicleListItemModel } from '../types/vehicles/vehicle-list-item.model';
import { PaginatedListModel } from '../types/paginated-list.model';
import { Vehicle } from '../types/vehicles/vehicle.model';
import { ApiResponse } from '../types/apiResponse.model';
import { VehicleForm } from '../types/vehicles/vehicleForm.model';
@Injectable({
  providedIn: 'root',
})
export class VehiclesClientService {
  constructor(private _http: HttpClient, @Inject('BASE_URL') private _baseUrl: string) { }

  public getVehiclesPaginated(pageNumber: number, pageSize: number): Observable<PaginatedListModel<VehicleListItemModel>> {
    return this._http.get<PaginatedListModel<VehicleListItemModel>>(
      `${this._baseUrl}api/Vehicles?PageNumber=${pageNumber}&PageSize=${pageSize}`
    );
  }

  public createVehicle(vehicleForm: VehicleForm): Observable<ApiResponse> {
    console.log(vehicleForm)
    return this._http.post<ApiResponse>(`${this._baseUrl}api/Vehicles`, vehicleForm);
  }

  public updateVehicle(vehicleId: Number, vehicleForm: VehicleForm): Observable<ApiResponse> {
    return this._http.put<ApiResponse>(`${this._baseUrl}api/Vehicles/${vehicleId}`, vehicleForm);
  }

  public getVehicleById(vehicleId: number): Observable<Vehicle> {
    return this._http.get<Vehicle>(`${this._baseUrl}api/Vehicles/${vehicleId}`);
  }

  public deleteVehicle(vehicleId: number): Observable<ApiResponse> {
    return this._http.delete<ApiResponse>(`${this._baseUrl}api/Vehicles/${vehicleId}`);
  }
}
