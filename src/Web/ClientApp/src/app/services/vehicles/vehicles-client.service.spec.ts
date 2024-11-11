import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { VehiclesClientService } from './vehicles-client.service';
import { PaginatedListModel } from '../../types/paginated-list.model';
import { VehicleListItemModel } from '../../types/vehicles/vehicle-list-item.model';
import { ApiResponse } from '../../types/apiResponse.model';
import { VehicleForm } from '../../types/vehicles/vehicleForm.model';
import { Vehicle } from '../../types/vehicles/vehicle.model';
import { ColourId } from '../../types/colours/colourId.enum';

describe('VehiclesClientService', () => {
  let service: VehiclesClientService;
  let httpMock: HttpTestingController;
  const mockBaseUrl = 'http://localhost:3000/';

  const mockVehicleList: PaginatedListModel<VehicleListItemModel> = {
    items: [
      { vehicleId: 1, manufacturerName: 'Toyota', model: 'Corolla', modelYear: 2020, colour: { colourId: 1, colourName: 'Red', colourHex: '#FF0000' } },
      { vehicleId: 2, manufacturerName: 'Ford', model: 'Focus', modelYear: 2019, colour: { colourId: 2, colourName: 'Blue', colourHex: '#0000FF' } },
    ],
    pageNumber: 1,
    totalPages: 1,
    totalCount: 2
  };

  const mockVehicleForm: VehicleForm = {
    manufacturerName: 'Honda',
    model: 'Civic',
    modelYear: 2022,
    colourId: ColourId.Red
  };

  const mockVehicleResponse: ApiResponse = {
    succeeded: true,
    errors: []
  };

  const mockVehicle: Vehicle = {
    vehicleId: 1,
    manufacturerName: 'Toyota',
    model: 'Corolla',
    modelYear: 2020,
    colourId: ColourId.Red
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        VehiclesClientService,
        { provide: 'BASE_URL', useValue: mockBaseUrl }
      ]
    });

    service = TestBed.inject(VehiclesClientService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should fetch paginated vehicles successfully', () => {
    service.getVehiclesPaginated(1, 10).subscribe(response => {
      expect(response).toEqual(mockVehicleList);
      expect(response.items.length).toBe(2);
    });


    const req = httpMock.expectOne(`${mockBaseUrl}api/Vehicles?PageNumber=1&PageSize=10`);
    expect(req.request.method).toBe('GET');
    req.flush(mockVehicleList);
  });

  it('should create a new vehicle successfully', () => {
    service.createVehicle(mockVehicleForm).subscribe(response => {
      expect(response).toEqual(mockVehicleResponse);
      expect(response.succeeded).toBeTrue();
    });


    const req = httpMock.expectOne(`${mockBaseUrl}api/Vehicles`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(mockVehicleForm);
    req.flush(mockVehicleResponse);
  });

  it('should update a vehicle successfully', () => {
    const vehicleId = 1;

    service.updateVehicle(vehicleId, mockVehicleForm).subscribe(response => {
      expect(response).toEqual(mockVehicleResponse);
      expect(response.succeeded).toBeTrue();
    });


    const req = httpMock.expectOne(`${mockBaseUrl}api/Vehicles/${vehicleId}`);
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(mockVehicleForm);
    req.flush(mockVehicleResponse);
  });

  it('should fetch a vehicle by ID successfully', () => {
    const vehicleId = 1;

    service.getVehicleById(vehicleId).subscribe(response => {
      expect(response).toEqual(mockVehicle);
      expect(response.vehicleId).toBe(1);
      expect(response.manufacturerName).toBe('Toyota');
    });


    const req = httpMock.expectOne(`${mockBaseUrl}api/Vehicles/${vehicleId}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockVehicle);
  });

  it('should delete a vehicle successfully', () => {
    const vehicleId = 1;

    service.deleteVehicle(vehicleId).subscribe(response => {
      expect(response).toEqual(mockVehicleResponse);
      expect(response.succeeded).toBeTrue();
    });


    const req = httpMock.expectOne(`${mockBaseUrl}api/Vehicles/${vehicleId}`);
    expect(req.request.method).toBe('DELETE');
    req.flush(mockVehicleResponse);
  });
});
