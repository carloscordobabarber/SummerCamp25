import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Apartment } from '../../models/apartment';

@Injectable({
  providedIn: 'root'
})
export class ApartmentClientsService {
  private apiUrl = 'https://devdemoapi4.azurewebsites.net/api/apartmentclients';

  constructor(private http: HttpClient) {}

  getApartments(): Observable<Apartment[]> {
    return this.http.get<Apartment[]>(this.apiUrl);
  }

}
