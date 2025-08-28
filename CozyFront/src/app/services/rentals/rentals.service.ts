
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Rental } from '../../models/rental';

@Injectable({
  providedIn: 'root'
})

export class RentalsService {
  private apiUrl = '/api/Rental';

  constructor(private http: HttpClient) {}

  getRentals(): Observable<Rental[]> {
    return this.http.get<Rental[]>(this.apiUrl);
  }

  getRental(id: number): Observable<Rental> {
    return this.http.get<Rental>(`${this.apiUrl}/${id}`);
  }

  getRentalsByUserId(userId: number) {
    return this.http.get<any[]>(`${this.apiUrl}/${userId}`);
  }

  createRental(rental: Rental): Observable<Rental> {
    return this.http.post<Rental>(this.apiUrl, rental);
  }

  updateRental(id: number, rental: Rental): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, rental);
  }

  deleteRental(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
  
  checkDate(apartmentId: number, date: string): Observable<{ exists: boolean; message?: string }> {
    return this.http.get<{ exists: boolean; message?: string }>(`${this.apiUrl}/CheckDate`, {
      params: { apartmentId: apartmentId.toString(), date }
    });
  }
}
