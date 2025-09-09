
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Rental } from '../../models/rental';

@Injectable({
  providedIn: 'root'
})

export class RentalsService {
  private apiUrl = 'https://devdemoapi4.azurewebsites.net/api/Rental';

  constructor(private http: HttpClient) {}

  getRentalsWithFilters(filters: {
    userId?: string;
    apartmentId?: string;
    statusId?: string;
    startDate?: string;
    endDate?: string;
    page?: number;
    pageSize?: number;
  }): Observable<{ items: Rental[]; totalCount: number }> {
    const params: any = {};
    if (filters.userId) params.userId = filters.userId;
    if (filters.apartmentId) params.apartmentId = filters.apartmentId;
    if (filters.statusId) params.statusId = filters.statusId;
    if (filters.startDate) params.startDate = filters.startDate;
    if (filters.endDate) params.endDate = filters.endDate;
    if (filters.page) params.page = filters.page;
    if (filters.pageSize) params.pageSize = filters.pageSize;
    return this.http.get<{ items: Rental[]; totalCount: number }>(`${this.apiUrl}`, { params });
  }

  getRental(id: number): Observable<Rental> {
    return this.http.get<Rental>(`${this.apiUrl}/${id}`);
  }

  getRentalsByUserId(userId: number): Observable<Rental[]> {
    return this.http.get<Rental[]>(`${this.apiUrl}/${userId}`);
  }

  createRental(rental: Rental): Observable<Rental> {
    return this.http.post<Rental>(this.apiUrl, rental);
  }

  updateRental(id: number, rental: Rental): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, rental);
  }

  deleteRental(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
  
  checkDate(apartmentId: number, date: string): Observable<{ exists: boolean; message?: string }> {
    return this.http.get<{ exists: boolean; message?: string }>(`${this.apiUrl}/CheckDate`, {
      params: { apartmentId: apartmentId.toString(), date }
    });
  }
}
