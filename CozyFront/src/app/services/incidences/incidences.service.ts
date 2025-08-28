import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Incidence } from '../../models/incidence';

@Injectable({
  providedIn: 'root'
})
export class IncidencesService {
  private apiUrl = 'https://devdemoapi4.azurewebsites.net/api/incidences';

  constructor(private http: HttpClient) {}

  getIncidences(page?: number, pageSize?: number, filters?: any): Observable<any> {
    let params = [];
    if (page !== undefined) params.push(`page=${page}`);
    if (pageSize !== undefined) params.push(`pageSize=${pageSize}`);
    if (filters) {
      if (filters.issueType) params.push(`issueType=${filters.issueType}`);
      if (filters.assignedCompany) params.push(`assignedCompany=${encodeURIComponent(filters.assignedCompany)}`);
      if (filters.apartmentId) params.push(`apartmentId=${encodeURIComponent(filters.apartmentId)}`);
      if (filters.rentalId) params.push(`rentalId=${encodeURIComponent(filters.rentalId)}`);
      if (filters.tenantId) params.push(`tenantId=${encodeURIComponent(filters.tenantId)}`);
      if (filters.statusId) params.push(`statusId=${encodeURIComponent(filters.statusId)}`);
    }
    const url = this.apiUrl + (params.length ? '?' + params.join('&') : '');
    return this.http.get<any>(url);
  }

  createIncidence(incidence: Incidence): Observable<Incidence> {
    return this.http.post<Incidence>(this.apiUrl, incidence);
  }
}
