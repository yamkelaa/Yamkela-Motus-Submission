import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store } from '@ngxs/store';
import { Observable, Subscription } from 'rxjs';
import { VehicleState } from 'src/app/stores/vehicles/vehicle.state';
import { VehicleActions } from 'src/app/stores/vehicles/vehicle.actions';
import { VehicleListItemModel } from 'src/app/types/vehicles/vehicle-list-item.model';
import { Router } from '@angular/router';

@Component({
  selector: 'vehicles-table',
  templateUrl: './vehicles-table.component.html',
  standalone: false
})
export class VehiclesTableComponent implements OnInit, OnDestroy {
  public vehicles$: Observable<VehicleListItemModel[]>;
  public loading$: Observable<boolean>;
  private paginationSubscription: Subscription;
  private pageSizeSubscription: Subscription;
  public currentPage: number = 1;
  public totalPages: number = 1;
  public pageSize: number;
  private isAlreadyLoading: boolean = false;

  constructor(private router: Router, private _store: Store) { }

  ngOnInit(): void {
    this.vehicles$ = this._store.select(VehicleState.getVehicleListItems);
    this.loading$ = this._store.select(VehicleState.getLoading);

    this.paginationSubscription = this._store.select(VehicleState.getPagination).subscribe(paginationState => {
      this.currentPage = paginationState.pageNumber;
      this.totalPages = paginationState.totalPages;
      this.loadVehiclesIfNotLoading();
    });

    this.pageSizeSubscription = this._store.select(VehicleState.getPageSize).subscribe(pageSize => {
      this.pageSize = pageSize;
      this.loadVehiclesIfNotLoading();
    });
    this.loadVehiclesIfNotLoading();
  }

  onRowClick(vehicle: VehicleListItemModel): void {
    this.router.navigate([`/vehicle-form/${vehicle.vehicleId}`]);
  }


  ngOnDestroy(): void {
    if (this.paginationSubscription) {
      this.paginationSubscription.unsubscribe();
    }
    if (this.pageSizeSubscription) {
      this.pageSizeSubscription.unsubscribe();
    }
  }

  private loadVehiclesIfNotLoading(): void {
    if (!this.isAlreadyLoading) {
      this.isAlreadyLoading = true;
      this.loadVehicles();
    }
  }

  private loadVehicles(): void {
    this._store.dispatch(new VehicleActions.LoadVehicles(this.currentPage, this.pageSize)).subscribe(() => {
      this.isAlreadyLoading = false;
    });
  }
}
