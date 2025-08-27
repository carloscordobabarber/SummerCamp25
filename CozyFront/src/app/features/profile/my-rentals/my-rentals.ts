import { Component } from '@angular/core';

@Component({
  selector: 'app-my-rentals',
  standalone: false,
  templateUrl: './my-rentals.html',
  styleUrl: './my-rentals.css'
})
export class MyRentals {
  vista: 'lista' | 'card' = 'lista';

  alquileres = [
    {
      id: 1,
      propiedad: 'Departamento en Palermo',
      fechaInicio: '2025-07-01',
      fechaFin: '2026-07-01',
      monto: 120000,
      pagado: true
    },
    {
      id: 2,
      propiedad: 'Casa en Tigre',
      fechaInicio: '2024-08-01',
      fechaFin: '2025-08-01',
      monto: 180000,
      pagado: false
    },
    // Puedes agregar más ejemplos
  ];

  toggleVista(tipo: 'lista' | 'card') {
    this.vista = tipo;
  }
}
