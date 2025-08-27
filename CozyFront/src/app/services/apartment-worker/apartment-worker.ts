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
}
