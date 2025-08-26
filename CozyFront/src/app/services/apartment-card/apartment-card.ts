import { Injectable } from '@angular/core';
import { Apartment } from '../../models/apartment';
import { Observable, map } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApartmentCard {
  private apiUrl = 'https://devdemoapi4.azurewebsites.net/api/apartmentcard';

  constructor(private http: HttpClient) {}

  getApartments(): Observable<Apartment[]> {
    return this.http.get<{ items: Apartment[] }>(this.apiUrl).pipe(
      map(response => response.items)
    );
  }
}
