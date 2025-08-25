import { Component } from '@angular/core';
import { Incidence } from '../../models/incidence';
import { IncidencesService } from '../../services/incidences/incidences.service';

@Component({
  selector: 'app-incidence-list',
  standalone: false,
  templateUrl: './incidence-list.html',
  styleUrl: './incidence-list.css'
})
export class IncidenceList {
  incidences: Incidence[] = [];
  cargando: boolean = false;

  constructor(private incidencesService: IncidencesService) {}

  ngOnInit(): void {
    this.cargando = true;
    this.incidencesService.getIncidences().subscribe({
      next: (data) => {
        this.incidences = data;
        console.log('Incidencias recibidas:', this.incidences);
        this.cargando = false;
      },
      error: (err) => {
        console.log('Error al obtener incidencias:', err);
        this.cargando = false;
      }
    });
  }
}
