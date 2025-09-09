import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { UserProfile } from '../../models/user';
import { UserRental } from '../../models/user-rental';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  // Cambia el puerto aquí para pruebas locales
  private readonly LOCAL_PORT = 7195;
  private readonly LOCAL_HOST = `https://localhost:${this.LOCAL_PORT}`;
  private userApiUrl = `${this.LOCAL_HOST}/api/users`;
  private loginUrl = `${this.LOCAL_HOST}/api/login/login`;
  // private userApiUrl = 'https://devdemoapi4.azurewebsites.net/api/users';
  // private loginUrl = 'https://devdemoapi4.azurewebsites.net/api/login/login';

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
    const token = localStorage.getItem('token');
    const headers = token ? new HttpHeaders({ 'Authorization': `Bearer ${token}` }) : undefined;
    return this.http.get<any>(this.userApiUrl, { params, headers });
  }

  /**
   * Obtiene los alquileres del usuario dado
   * @param userId ID del usuario
   */
  /**
   * Obtiene los alquileres detallados del usuario dado
   * @param userId ID del usuario
   */
  getUserRentals(): Observable<UserRental[]> {
    const token = localStorage.getItem('token');
    const headers = token ? new HttpHeaders({ 'Authorization': `Bearer ${token}` }) : undefined;
    // Obtener el id del usuario desde el token
    let userId = '';
    if (token) {
      try {
        const payload = JSON.parse(atob(token.split('.')[1]));
        userId = payload.sub;
      } catch (e) {
        console.error('Error decodificando el token', e);
      }
    }
    return this.http.get<UserRental[]>(`/api/UsersRentals/user/${userId}`, { headers });
  }

    /**
     * Realiza login y devuelve un observable con el token JWT
     */
    login(email: string, password: string): Observable<{ token: string }> {
      const body = { email, password };
      console.log('Login request body:', body);
      return this.http.post<{ token: string }>(this.loginUrl, body);
  }

  getUser(): Observable<UserProfile> {
    const token = localStorage.getItem('token');
    const headers = token ? new HttpHeaders({ 'Authorization': `Bearer ${token}` }) : undefined;
    // Obtener el id del usuario desde el token
    let userId = '';
    if (token) {
      try {
        const payload = JSON.parse(atob(token.split('.')[1]));
        userId = payload.sub;
      } catch (e) {
        console.error('Error decodificando el token', e);
      }
    }
    return this.http.get<UserProfile>(`${this.userApiUrl}/${userId}`, { headers });
  }

  createUser(user: any): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = token ? new HttpHeaders({ 'Authorization': `Bearer ${token}` }) : undefined;
    return this.http.post<any>(this.userApiUrl, user, { headers });
  }

  updateUser(user: UserProfile): Observable<void> {
    const token = localStorage.getItem('token');
    const headers = token ? new HttpHeaders({ 'Authorization': `Bearer ${token}` }) : undefined;
    return this.http.put<void>(`${this.userApiUrl}/${user.id}`, user, { headers });
  }

  updateUserRole(id: number, role: string): Observable<void> {
    const token = localStorage.getItem('token');
    const headers = token ? new HttpHeaders({ 'Authorization': `Bearer ${token}` }) : undefined;
    return this.http.put<void>(`${this.userApiUrl}/clientRole/${id}`, { role }, { headers });
  }
}
