
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

export interface ApartmentCardDto {
  id: number;
  code: string;
  door: string;
  floor: string;
  price: number;
  area: number;
  numberOfRooms: number;
  numberOfBathrooms: number;
  buildingId: number;
  hasLift: boolean;
  hasGarage: boolean;
  isAvailable: boolean;
  streetName: string;
  districtId: number;
  districtName: string;
  imageUrls: string[];
}

@Injectable({
  providedIn: 'root'
})
export class ApartmentCard {
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
  } = {}): Observable<{ items: ApartmentCardDto[], totalCount: number }> {
    let httpParams = new HttpParams();
    Object.entries(params).forEach(([key, value]) => {
      if (value !== undefined && value !== null) {
        httpParams = httpParams.set(key, value as any);
      }
    });
    return this.http.get<any>(this.apiUrl, { params: httpParams }).pipe(
      map(result => {
        if (result && Array.isArray(result.items) && typeof result.totalCount === 'number') {
          return result as { items: ApartmentCardDto[], totalCount: number };
        }
        if (Array.isArray(result)) {
          return { items: result, totalCount: result.length };
        }
        return { items: [], totalCount: 0 };
      })
    );
  }

  getApartment(id: number): Observable<ApartmentCardDto> {
    return this.http.get<ApartmentCardDto>(`${this.apiUrl}/${id}`);
  }
}
