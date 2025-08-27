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

  getIncidences(page?: number, pageSize?: number): Observable<any> {
    let url = this.apiUrl;
    if (page !== undefined && pageSize !== undefined) {
      url += `?page=${page}&pageSize=${pageSize}`;
    }
    return this.http.get<any>(url);
  }

  createIncidence(incidence: Incidence): Observable<Incidence> {
    return this.http.post<Incidence>(this.apiUrl, incidence);
  }
}
