import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Colour } from "../types/colours/colours.model";

@Injectable({
  providedIn: "root",
})
export class ColoursClientService {
  constructor(private _http: HttpClient,
    @Inject('BASE_URL') private _baseUrl: string) {
  }

  public getVehicleColours(): Observable<Colour[]> {
    return this._http.get<Colour[]>(`${this._baseUrl}api/Colours`);
  }
}
