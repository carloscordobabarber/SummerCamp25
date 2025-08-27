
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface RentalDto {
  id: number;
  userId: number;
  apartmentId: number;
  startDate: string;
  endDate: string;
  statusId: number;
}

@Injectable({
  providedIn: 'root'
})
export class RentalsService {
  private apiUrl = '/api/Rental';

  constructor(private http: HttpClient) {}

  getRentals(): Observable<RentalDto[]> {
    return this.http.get<RentalDto[]>(this.apiUrl);
  }

  getRental(id: number): Observable<RentalDto> {
    return this.http.get<RentalDto>(`${this.apiUrl}/${id}`);
  }

  createRental(rental: RentalDto): Observable<RentalDto> {
    return this.http.post<RentalDto>(this.apiUrl, rental);
  }

  updateRental(id: number, rental: RentalDto): Observable<void> {
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
