import { Component, Input } from '@angular/core';
import { Incidence } from '../../models/incidence';
import { IncidenceForm } from './components/incidence-form/incidence-form';
import { IncidenceViewer } from './components/incidence-viewer/incidence-viewer';
import { IncidenceSidebar } from './components/incidence-sidebar/incidence-sidebar';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-incidences',
  standalone: true,
  templateUrl: './incidences.html',
  styleUrl: './incidences.css',
  imports: [CommonModule, IncidenceForm, IncidenceViewer, IncidenceSidebar]
})
export class Incidences {
  @Input() user: any;
  tab: 'nueva' | 'historial' = 'nueva';
    selectedIncidence: Incidence | null = null;
    incidences: Incidence[] = [];

  constructor() {
    window.addEventListener('incidenceSelected', (e: any) => {
        const customEvent = e as CustomEvent<Incidence>;
        this.selectedIncidence = customEvent.detail;
    });
    window.addEventListener('incidencesLoaded', (e: any) => {
        const customEvent = e as CustomEvent<Incidence[]>;
        this.incidences = customEvent.detail;
    });
  }
}
