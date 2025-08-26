import { Injectable } from '@angular/core';
import { Apartment } from '../../models/apartment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApartmentWorker {
  private apiUrl = 'https://devdemoapi4.azurewebsites.net/api/apartmentworkers';

  constructor(private http: HttpClient) {}

  getApartments(): Observable<Apartment[]> {
    return this.http.get<Apartment[]>(this.apiUrl);
  }
}
