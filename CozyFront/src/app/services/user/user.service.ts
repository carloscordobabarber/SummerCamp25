import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserProfile } from '../../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://devdemoapi4.azurewebsites.net/api/users';
  private loginUrl = 'https://devdemoapi4.azurewebsites.net/api/login/login';

  constructor(private http: HttpClient) {}

  getUsers(page?: number, pageSize?: number, filters?: any): Observable<any> {
    let params: any = {};
    if (page !== undefined && pageSize !== undefined) {
      params.page = page;
      params.pageSize = pageSize;
    }
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key] !== undefined && filters[key] !== '') {
          params[key] = filters[key];
        }
      });
    }
    return this.http.get<any>(this.apiUrl, { params });
  }

  /**
   * Realiza login y devuelve un observable con el id y role del usuario
   */
  login(email: string, password: string): Observable<{ id: number; role: string }> {
    return this.http.post<{ id: number; role: string }>(this.loginUrl, { email, password });
  }

  getUser(id: number): Observable<UserProfile> {
    return this.http.get<UserProfile>(`${this.apiUrl}/${id}`);
  }

  createUser(user: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, user);
  }

  updateUser(user: UserProfile): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${user.id}`, user);
  }
}
