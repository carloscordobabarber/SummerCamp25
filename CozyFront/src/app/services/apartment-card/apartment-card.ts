
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ApartmentCard } from '../../models/apartment-card';

@Injectable({
  providedIn: 'root'
})
export class ApartmentCardService {
  private apiUrl = 'https://devdemoapi4.azurewebsites.net/api/apartmentcard';

  constructor(private http: HttpClient) {}

  getApartments(params: {
    page?: number,
    pageSize?: number,
    districtId?: number,
    area?: number,
    hasLift?: boolean,
    minPrice?: number,
    maxPrice?: number,
    hasGarage?: boolean,
    numberOfRooms?: number,
    numberOfBathrooms?: number
  } = {}): Observable<{ items: ApartmentCard[], totalCount: number }> {
    let httpParams = new HttpParams();
    Object.entries(params).forEach(([key, value]) => {
      if (value !== undefined && value !== null) {
        httpParams = httpParams.set(key, value as any);
      }
    });
    return this.http.get<any>(this.apiUrl, { params: httpParams }).pipe(
      map(result => {
        if (result && Array.isArray(result.items) && typeof result.totalCount === 'number') {
          return result as { items: ApartmentCard[], totalCount: number };
        }
        if (Array.isArray(result)) {
          return { items: result, totalCount: result.length };
        }
        return { items: [], totalCount: 0 };
      })
    );
  }

  getApartment(id: number): Observable<ApartmentCard> {
    return this.http.get<ApartmentCard>(`${this.apiUrl}/${id}`);
  }
}
