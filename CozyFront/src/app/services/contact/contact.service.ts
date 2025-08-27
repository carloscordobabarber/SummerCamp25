import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

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
  private apiUrl = '/api/Contacts';

  constructor(private http: HttpClient) {}

  getContacts(params?: {
    page?: number;
    pageSize?: number;
    contactReason?: string;
  }): Observable<{ totalCount: number; items: ContactDto[] }> {
    let httpParams = new HttpParams();
    if (params) {
      if (params.page) httpParams = httpParams.set('page', params.page.toString());
      if (params.pageSize) httpParams = httpParams.set('pageSize', params.pageSize.toString());
      if (params.contactReason) httpParams = httpParams.set('contactReason', params.contactReason);
    }
    return this.http.get<{ totalCount: number; items: ContactDto[] }>(this.apiUrl, { params: httpParams });
  }

  getContact(id: number): Observable<ContactDto> {
    return this.http.get<ContactDto>(`${this.apiUrl}/${id}`);
  }

  postContact(contact: ContactDto): Observable<ContactDto> {
    return this.http.post<ContactDto>(this.apiUrl, contact);
  }
}
