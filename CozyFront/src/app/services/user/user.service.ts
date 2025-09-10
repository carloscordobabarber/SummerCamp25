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
  /**
   * Devuelve el token JWT almacenado en localStorage
   */
  getToken(): string | null {
    return localStorage.getItem('token');
  }
  /**
   * Obtiene el id del usuario actual desde el token JWT (Posiblemente obsoleto con GetUser)
   */
  getUserIdFromToken(): number | null {
    const token = localStorage.getItem('token');
    if (!token) return null;
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.sub ? parseInt(payload.sub, 10) : null;
    } catch {
      return null;
    }
  }
  /**
   * Obtiene el rol del usuario actual desde el token JWT
   */
  getRoleFromToken(): string | null {
    const token = localStorage.getItem('token');
    if (!token) return null;
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] || null;
    } catch {
      return null;
    }
  }

   private userApiUrl = 'https://devdemoapi4.azurewebsites.net/api/users';
   private loginUrl = 'https://devdemoapi4.azurewebsites.net/api/login/login';

  constructor(private http: HttpClient) {}

  //modificado para funcionar con JWT
  getUsers(page?: number, pageSize?: number, filters?: any): Observable<{ items: UserProfile[]; totalCount: number }> {
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
  return this.http.get<{ items: UserProfile[]; totalCount: number }>(this.userApiUrl, { params, headers });
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

  createUser(user: UserProfile): Observable<UserProfile> {
    const token = localStorage.getItem('token');
    const headers = token ? new HttpHeaders({ 'Authorization': `Bearer ${token}` }) : undefined;
    return this.http.post<UserProfile>(this.userApiUrl, user, { headers });
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
