import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';  // Already imported
import { VehiclesClientService } from "./services/vehicles/vehicles-client.service";
import { NgxsModule } from '@ngxs/store';
import { environment } from 'src/environments/environment';
import { VehicleState } from './stores/vehicles/vehicle.state';
import { VehiclesTableComponent } from './components/home/vehicles-table/vehicles-table.component';
import { PaginationComponent } from './components/home/pagination/pagination.component';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';
import { ColourState } from './stores/colours/colour.state';
import { ToastrModule } from 'ngx-toastr';
import { ColoursClientService } from './services/colours/colour-client.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    VehiclesTableComponent,
    PaginationComponent,
    VehicleFormComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', pathMatch: 'full', redirectTo: 'vehicles' },
      { path: 'vehicles', component: HomeComponent },
      { path: 'vehicle-form', component: VehicleFormComponent },
      { path: 'vehicle-form/:vehicleId', component: VehicleFormComponent },
      { path: '**', redirectTo: 'vehicles' },
    ]),
    BrowserAnimationsModule,
    NgxsModule.forRoot([
      VehicleState,
      ColourState
    ], { developmentMode: !environment.production }),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-left',
      timeOut: 700,
      closeButton: true,
      progressBar: true,
    })
  ],
  providers: [
    VehiclesClientService,
    ColoursClientService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
