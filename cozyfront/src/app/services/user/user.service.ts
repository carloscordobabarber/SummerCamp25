import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserProfile } from '../../models/user';
import { Rental } from '../../models/rental';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private userApiUrl = 'https://devdemoapi4.azurewebsites.net/api/users';
  private loginUrl = 'https://devdemoapi4.azurewebsites.net/api/login/login';

  constructor(private http: HttpClient) {}

  getUsers(page?: number, pageSize?: number): Observable<any> {
    let url = this.userApiUrl;
    if (page !== undefined && pageSize !== undefined) {
      url += `?page=${page}&pageSize=${pageSize}`;
    }
    return this.http.get<any>(url);
  }

  /**
   * Obtiene los alquileres del usuario dado
   * @param userId ID del usuario
   */
  getUserRentals(userId: number): Observable<Rental[]> {
    return this.http.get<Rental[]>(`${this.userApiUrl}/${userId}/rentals`);
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
}
