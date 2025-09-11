import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CF_Incidence } from '../../models/cf-incidence';

@Injectable({
  providedIn: 'root'
})
export class CfIncidenceService {
  private apiUrl = 'https://devdemoapi2.azurewebsites.net/api/incidences';

  constructor(private http: HttpClient) {}

  createIncidence(incidence: CF_Incidence): Observable<any> {
    return this.http.post(this.apiUrl, incidence);
  }
}
