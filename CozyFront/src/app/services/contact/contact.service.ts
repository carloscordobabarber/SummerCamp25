import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Contact } from '../../features/contact/contact';

export interface ContactDto {
  id?: number;
  name?: string;
  email: string;
  phone?: string;
  contactReason?: string;
  message?: string;
}

@Injectable({
  providedIn: 'root',
})
export class ContactService {
  private apiUrl = 'https://devdemoapi4.azurewebsites.net/api/contacts';

  constructor(private http: HttpClient) {}

  getContactsWithFilters(filters: {
    email?: string;
    contactReason?: string;
    page?: number;
    pageSize?: number;
  }): Observable<{ items: Contact[]; totalCount: number }> {
    const params: any = {};
    if (filters.email) params.email = filters.email;
    if (filters.contactReason) params.contactReason = filters.contactReason;
    if (filters.page) params.page = filters.page;
    if (filters.pageSize) params.pageSize = filters.pageSize;
    return this.http.get<{ items: Contact[]; totalCount: number }>(`${this.apiUrl}`, { params });
  }

  getContact(id: number): Observable<ContactDto> {
    return this.http.get<ContactDto>(`${this.apiUrl}/${id}`);
  }

  postContact(contact: ContactDto): Observable<ContactDto> {
    return this.http.post<ContactDto>(this.apiUrl, contact);
  }
}
