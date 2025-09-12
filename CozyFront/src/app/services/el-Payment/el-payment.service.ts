import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
// El error ocurre porque no existe el archivo '../../models/el-payment' o no tiene declaraciones de tipo.
// Asegúrate de que el archivo 'el-payment.ts' exista en 'src/app/models' y exporte la clase o interfaz ElPayment.
// Si solo usas ElPaymentDto en este servicio, puedes eliminar la línea de importación innecesaria.

export interface ElPaymentDto {
  description: string;
  apartmentCode: string;
  amount: number;
}

@Injectable({
  providedIn: 'root',
})
export class ElPaymentService {
  private apiUrl = 'https://devdemoapi3.azurewebsites.net/api/transactions';
  constructor(private http: HttpClient) {}

  postPayment(payment: ElPaymentDto): Observable<ElPaymentDto> {
    return this.http.post<ElPaymentDto>(this.apiUrl, payment);
  }
}
