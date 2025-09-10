import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ChangePass } from '../../models/change-pass';

@Injectable({ providedIn: 'root' })
export class ChangePassService {
  private apiUrl = 'https://devdemoapi4.azurewebsites.net/api/changepass';

  constructor(private http: HttpClient) {}

  changePassword(dto: ChangePass, token: string): Observable<{ message: string }> {
    const headers = { Authorization: `Bearer ${token}` };
    return this.http.post<{ message: string }>(this.apiUrl, dto, { headers });
  }
}
