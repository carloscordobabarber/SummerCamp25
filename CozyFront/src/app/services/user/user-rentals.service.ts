import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserRental } from '../../models/user-rental';

@Injectable({ providedIn: 'root' })
export class UserRentalsService {
  private apiUrl = 'https://devdemoapi4.azurewebsites.net/api/UsersRentals/user';

  constructor(private http: HttpClient) {}

  getRentalsByUserId(userId: number): Observable<UserRental[]> {
    return this.http.get<UserRental[]>(`${this.apiUrl}/${userId}`);
  }
}
