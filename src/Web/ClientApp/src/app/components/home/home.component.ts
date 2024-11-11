import { Component, OnInit } from '@angular/core';
import { Store } from "@ngxs/store";
import { VehicleState } from 'src/app/stores/vehicles/vehicle.state';
import { VehicleActions } from 'src/app/stores/vehicles/vehicle.actions';
import { VehicleListItemModel } from "../../types/vehicles/vehicle-list-item.model";
import { Observable } from "rxjs";
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  public vehicles$: Observable<VehicleListItemModel[]> = this._store.select(VehicleState.getVehicleListItems);
  public currentPage: number = 1;
  //This is just a default page size
  public pageSize: number = 10;

  constructor(private _store: Store, private router: Router) { }

  ngOnInit(): void {
    this._store.dispatch(new VehicleActions.LoadVehicles(this.currentPage, this.pageSize));
    //Function to set page size globally
    this.setPageSize(10)
  }
  setPageSize(newPageSize: number): void {
    this.pageSize = newPageSize;
    this._store.dispatch(new VehicleActions.SetPageSize(newPageSize));
    this._store.dispatch(new VehicleActions.LoadVehicles(this.currentPage, newPageSize));
  }

  navigateToCreateVehicle() {
    this.router.navigate(['/vehicle-form'])
  }

}
