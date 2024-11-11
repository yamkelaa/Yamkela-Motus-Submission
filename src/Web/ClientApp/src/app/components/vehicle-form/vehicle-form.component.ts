import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngxs/store';
import { ColourState } from 'src/app/stores/colours/colour.state';
import { ColourActions } from 'src/app/stores/colours/colour.actions';
import { Colour } from 'src/app/types/colours/colours.model';
import { Observable, Subscription } from 'rxjs';
import { VehicleActions } from 'src/app/stores/vehicles/vehicle.actions';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'vehicle-form',
  templateUrl: './vehicle-form.component.html',
})
export class VehicleFormComponent implements OnInit, OnDestroy {
  vehicleForm: FormGroup;
  colours$: Observable<Colour[]>;
  isLoadingColours$: Observable<boolean>;
  isLoadingVehicle$: Observable<boolean>;
  responseMessage$: Observable<string | null>;
  private coloursSubscription: Subscription;
  private responseMessageSub: Subscription;
  vehicleId: number | null = null;
  submitted: boolean = false;

  constructor(
    private fb: FormBuilder,
    private store: Store,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.vehicleForm = this.fb.group({
      manufacturerName: ['', Validators.required],
      model: ['', Validators.required],
      modelYear: [
        '',
        [Validators.required, Validators.min(2015), Validators.max(new Date().getFullYear())],
      ],
      colourId: [null],
    });

    this.colours$ = this.store.select(ColourState.getColours);
    this.isLoadingColours$ = this.store.select(ColourState.getIsLoading);
    this.isLoadingVehicle$ = this.store.select(state => state.vehicle.isLoading);
    this.responseMessage$ = this.store.select(state => state.vehicle.responseMessage);

    this.loadColoursIfNotLoading();

    this.route.paramMap.subscribe(params => {
      const id = params.get('vehicleId');
      if (id) {
        this.vehicleId = +id;
        this.loadVehicleForEdit(this.vehicleId);
      }
    });

    this.responseMessageSub = this.responseMessage$.subscribe(message => {
      if (message) {
        this.showToastMessage(message);
      }
    });
  }

  ngOnDestroy(): void {
    if (this.coloursSubscription) {
      this.coloursSubscription.unsubscribe();
    }
    if (this.responseMessageSub) {
      this.responseMessageSub.unsubscribe();
    }
  }

  private loadColoursIfNotLoading(): void {
    this.store.select(ColourState.getIsLoading).subscribe(isLoading => {
      if (!isLoading) {
        this.store.dispatch(new ColourActions.LoadColours());
      }
    });
  }

  private loadVehicleForEdit(id: number): void {
    this.store.dispatch(new VehicleActions.LoadVehicleById(id)).subscribe(() => {
      const vehicle = this.store.selectSnapshot(state => state.vehicle.selectedVehicle);
      if (vehicle) {
        this.vehicleForm.patchValue({
          manufacturerName: vehicle.manufacturerName,
          model: vehicle.model,
          modelYear: vehicle.modelYear,
          colourId: vehicle.colourId
        });
      }
    });
  }

  get f() {
    return this.vehicleForm.controls;
  }

  onColourSelect(colour: Colour): void {
    this.vehicleForm.patchValue({
      colourId: colour.colourId
    });
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.vehicleForm.invalid) {
      this.markAllFieldsAsTouched();
      return;
    }
    if (this.vehicleId) {
      console.log(this.vehicleForm.value)
      this.store.dispatch(new VehicleActions.UpdateVehicle(this.vehicleId, this.vehicleForm.value));
    } else {
      this.store.dispatch(new VehicleActions.CreateVehicle(this.vehicleForm.value));
    }
  }

  private markAllFieldsAsTouched() {
    Object.values(this.vehicleForm.controls).forEach(control => {
      control.markAsTouched();
    });
  }
  private showToastMessage(message: string): void {
    if (!message.includes('Error')) {
      this.toastr.success(message, 'Success').onHidden.subscribe(() => {
        this.vehicleForm.reset();
        this.router.navigate(['/vehicles']);
      });
    } else {
      this.toastr.error(message, 'Error').onHidden.subscribe(() => {
        this.router.navigate(['/vehicles']);
      });
    }
  }

  onDelete(): void {
    if (this.vehicleId) {
      console.log(this.vehicleId);
      this.store.dispatch(new VehicleActions.DeleteVehicle(this.vehicleId)).subscribe(() => {
        this.router.navigate(['/vehicles']);
      });
    }
  }
}
