import { Component, Input } from '@angular/core';
import { IncidenceForm } from './components/incidence-form/incidence-form';
import { IncidenceViewer } from './components/incidence-viewer/incidence-viewer';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-incidences',
  standalone: true,
  templateUrl: './incidences.html',
  styleUrl: './incidences.css',
  imports: [CommonModule, IncidenceForm, IncidenceViewer]
})
export class Incidences {
  @Input() user: any;
  tab: 'nueva' | 'historial' = 'nueva';
  mockIncidence = {
    id: 1,
    spokesperson: 'Juan Pérez',
    description: 'No funciona la luz del pasillo.',
    issueType: 0,
    assignedCompany: 'Electricistas S.A.',
    createdAt: new Date(),
    updatedAt: null,
    apartmentId: 101,
    rentalId: 2001,
    tenantId: 3001,
    statusId: 'A'
  };
}
