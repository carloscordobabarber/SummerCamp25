import { Injectable } from '@angular/core';
import { Apartment } from '../../models/apartment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApartmentWorker {
  private apiUrl = 'https://devdemoapi4.azurewebsites.net/api/apartmentworkers';

  constructor(private http: HttpClient) { }

  getApartments(page?: number, pageSize?: number): Observable<Apartment[]> {
    let url = this.apiUrl;
    if (page !== undefined && pageSize !== undefined) {
      url += `?page=${page}&pageSize=${pageSize}`;
    }
    return this.http.get<Apartment[]>(url);
  }

  getApartmentsWithFilters(filters: any): Observable<{ items: Apartment[]; totalCount: number } | Apartment[]> {
    let params = [];
    if (filters.page) params.push(`page=${filters.page}`);
    if (filters.pageSize) params.push(`pageSize=${filters.pageSize}`);
    if (filters.minPrice !== undefined) params.push(`minPrice=${filters.minPrice}`);
    if (filters.maxPrice !== undefined) params.push(`maxPrice=${filters.maxPrice}`);
    if (filters.area !== undefined) params.push(`area=${filters.area}`);
    if (filters.numberOfRooms !== undefined) params.push(`numberOfRooms=${filters.numberOfRooms}`);
    if (filters.numberOfBathrooms !== undefined) params.push(`numberOfBathrooms=${filters.numberOfBathrooms}`);
    if (filters.street) params.push(`streetName=${encodeURIComponent(filters.street)}`);
    if (filters.door) params.push(`door=${encodeURIComponent(filters.door)}`);
    if (filters.code) params.push(`code=${encodeURIComponent(filters.code)}`);
    const url = this.apiUrl + (params.length ? '?' + params.join('&') : '');
    return this.http.get<{ items: Apartment[]; totalCount: number } | Apartment[]>(url);
  }
}
