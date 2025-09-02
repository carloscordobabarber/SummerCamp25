import { Component, Input } from '@angular/core';
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
  selectedIncidence: any = null;
  incidences: any[] = [];

  constructor() {
    window.addEventListener('incidenceSelected', (e: any) => {
      this.selectedIncidence = e.detail;
    });
    window.addEventListener('incidencesLoaded', (e: any) => {
      this.incidences = e.detail;
    });
  }
}
