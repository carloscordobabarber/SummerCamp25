import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ChangePass } from '../../models/change-pass';

@Injectable({ providedIn: 'root' })
export class ChangePassService {
  private apiUrl = 'https://devdemoapi4.azurewebsites.net/api/changepass';

  constructor(private http: HttpClient) {}

  changePassword(dto: ChangePass): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(this.apiUrl, dto);
  }
}
