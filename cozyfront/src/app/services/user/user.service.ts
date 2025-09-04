import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserProfile } from '../../models/user';
import { UserRental } from '../../models/user-rental';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private userApiUrl = 'https://devdemoapi4.azurewebsites.net/api/users';
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
    return this.http.get<any>(this.userApiUrl, { params });
  }

  /**
   * Obtiene los alquileres del usuario dado
   * @param userId ID del usuario
   */
  /**
   * Obtiene los alquileres detallados del usuario dado
   * @param userId ID del usuario
   */
  getUserRentals(userId: number): Observable<UserRental[]> {
    return this.http.get<UserRental[]>(`/api/UsersRentals/user/${userId}`);
  }

  /**
   * Realiza login y devuelve un observable con el id y role del usuario
   */
  login(email: string, password: string): Observable<{ id: number; role: string }> {
    return this.http.post<{ id: number; role: string }>(this.loginUrl, { email, password });
  }

  getUser(id: number): Observable<UserProfile> {
    return this.http.get<UserProfile>(`${this.userApiUrl}/${id}`);
  }

  createUser(user: any): Observable<any> {
    return this.http.post<any>(this.userApiUrl, user);
  }

  updateUser(user: UserProfile): Observable<void> {
    return this.http.put<void>(`${this.userApiUrl}/${user.id}`, user);
  }

  updateUserRole(id: number, role: string): Observable<void> {
    return this.http.put<void>(`${this.userApiUrl}/clientRole/${id}`, { role });
  }
}
