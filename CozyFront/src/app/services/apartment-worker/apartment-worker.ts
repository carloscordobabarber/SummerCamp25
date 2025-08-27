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

  getApartments(page?: number, pageSize?: number): Observable<any> {
    let url = this.apiUrl;
    if (page !== undefined && pageSize !== undefined) {
      url += `?page=${page}&pageSize=${pageSize}`;
    }
    return this.http.get<any>(url);
  }

  getApartmentsWithFilters(filters: any): Observable<any> {
    let params = [];
    if (filters.page) params.push(`page=${filters.page}`);
    if (filters.pageSize) params.push(`pageSize=${filters.pageSize}`);
    if (filters.title) params.push(`title=${encodeURIComponent(filters.title)}`);
    if (filters.door) params.push(`door=${encodeURIComponent(filters.door)}`);
    if (filters.area) params.push(`area=${filters.area}`);
    if (filters.rooms) params.push(`rooms=${filters.rooms}`);
    if (filters.baths) params.push(`baths=${filters.baths}`);
    if (filters.priceMin !== undefined) params.push(`priceMin=${filters.priceMin}`);
    if (filters.priceMax !== undefined) params.push(`priceMax=${filters.priceMax}`);
    const url = this.apiUrl + (params.length ? '?' + params.join('&') : '');
    return this.http.get<any>(url);
  }
}
