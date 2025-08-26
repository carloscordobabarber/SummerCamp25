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

  getIncidences(): Observable<Incidence[]> {
    return this.http.get<Incidence[]>(this.apiUrl);
  }

  createIncidence(incidence: Incidence): Observable<Incidence> {
    return this.http.post<Incidence>(this.apiUrl, incidence);
  }
}
