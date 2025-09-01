import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Payment } from '../../models/payment';

@Injectable({
  providedIn: 'root'
})
export class PaymentsService {
  private apiUrl = 'https://devdemoapi4.azurewebsites.net/api/payments';

  constructor(private http: HttpClient) {}

  getPaymentsWithFilters(filters: {
    statusId?: string;
    rentalId?: string;
    startDate?: string;
    endDate?: string;
    bankAccount?: string;
    page?: number;
    pageSize?: number;
  }): Observable<{ items: Payment[]; totalCount: number }> {
    const params: any = {};
    if (filters.statusId) params.statusId = filters.statusId;
    if (filters.rentalId) params.rentalId = filters.rentalId;
    if (filters.startDate) params.startDate = filters.startDate;
    if (filters.endDate) params.endDate = filters.endDate;
    if (filters.bankAccount) params.bankAccount = filters.bankAccount;
    if (filters.page) params.page = filters.page;
    if (filters.pageSize) params.pageSize = filters.pageSize;
    return this.http.get<{ items: Payment[]; totalCount: number }>(`${this.apiUrl}`, { params });
  }

  getPayment(id: number): Observable<Payment> {
    return this.http.get<Payment>(`${this.apiUrl}/${id}`);
  }

  createPayment(payment: Payment): Observable<Payment> {
    return this.http.post<Payment>(this.apiUrl, payment);
  }

  updatePayment(id: number, payment: Payment): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, payment);
  }

  deletePayment(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
