import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApartmentAvailabilityService {
  private baseUrl = 'https://devdemoapi4.azurewebsites.net/api/Apartments';

  constructor(private http: HttpClient) {}

  setAvailable(apartmentId: number, available: boolean): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${apartmentId}/set-available`, { available });
  }

  setUnavailable(apartmentId: number): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${apartmentId}/set-unavailable`, {});
  }
}
