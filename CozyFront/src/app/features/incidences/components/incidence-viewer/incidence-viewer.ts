import { Component, Input } from '@angular/core';
import { DatePipe, CommonModule } from '@angular/common';

@Component({
  selector: 'app-incidence-viewer',
  standalone: true,
  templateUrl: './incidence-viewer.html',
  styleUrl: './incidence-viewer.css',
  imports: [CommonModule, DatePipe]
})
export class IncidenceViewer {
  @Input() incidence: any;

  incidenceTypes = [
    'Avería eléctrica',
    'Fuga de agua',
    'Problema de calefacción',
    'Daños estructurales',
    'Plagas',
    'Problemas de acceso',
    'Otros'
  ];

  get issueTypeName(): string {
    if (!this.incidence) return '';
    const idx = Number(this.incidence.issueType);
    return this.incidenceTypes[idx] ?? 'Desconocido';
  }
}
