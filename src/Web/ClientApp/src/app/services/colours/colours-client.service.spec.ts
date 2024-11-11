import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ColoursClientService } from './colour-client.service';
import { Colour } from '../../types/colours/colours.model';

describe('ColoursClientService', () => {
  let service: ColoursClientService;
  let httpMock: HttpTestingController;

  const mockColours: Colour[] = [
    { colourId: 1, colourName: 'Red', colourHex: '#FF0000' },
    { colourId: 2, colourName: 'Blue', colourHex: '#0000FF' },
    { colourId: 3, colourName: 'Green', colourHex: '#00FF00' }
  ];

  const mockBaseUrl = 'http://localhost:3000/';  // Mock the base URL for your service

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        ColoursClientService,
        { provide: 'BASE_URL', useValue: mockBaseUrl }  // Mock the BASE_URL provider
      ]
    });

    service = TestBed.inject(ColoursClientService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // Ensure no unmatched requests remain.
  });

  it('should fetch vehicle colours successfully', () => {
    service.getVehicleColours().subscribe(colours => {
      expect(colours.length).toBe(3);
      expect(colours).toEqual(mockColours);
    });

    // Expect the correct HTTP request to be made
    const req = httpMock.expectOne(`${mockBaseUrl}api/Colours`); // Use mockBaseUrl here
    expect(req.request.method).toBe('GET');
    req.flush(mockColours); // Return mock data
  });

  it('should handle an error if the request fails', () => {
    const errorMessage = 'Failed to fetch colours';

    service.getVehicleColours().subscribe({
      next: () => fail('should have failed with the error'),
      error: (error) => {
        expect(error.status).toBe(500);
        expect(error.error).toBe(errorMessage);
      }
    });

    // Simulate a failed HTTP request
    const req = httpMock.expectOne(`${mockBaseUrl}api/Colours`);  // Use mockBaseUrl here
    expect(req.request.method).toBe('GET');
    req.flush(errorMessage, { status: 500, statusText: 'Server Error' });
  });
});
