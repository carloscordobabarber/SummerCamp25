import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApartmentCard } from '../../models/apartment-card';

@Injectable({
  providedIn: 'root'
})
export class ApartmentCardClientService {
  private apiUrl = '/api/ApartmentCardClient';

  constructor(private http: HttpClient) {}

  getUserApartments(userId: number): Observable<ApartmentCard[]> {
    return this.http.get<ApartmentCard[]>(`${this.apiUrl}/UserApartments/${userId}`);
  }
}
