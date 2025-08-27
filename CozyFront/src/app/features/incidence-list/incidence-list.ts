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
  page = 1;
  pageSize = 10;
  totalCount = 0;

  constructor(private incidencesService: IncidencesService) {}

  ngOnInit(): void {
    this.cargando = true;
    this.loadIncidences();
  }

  loadIncidences() {
    this.incidencesService.getIncidences(this.page, this.pageSize).subscribe((result: any) => {
      if (result.items && result.totalCount !== undefined) {
        this.incidences = result.items;
        this.totalCount = result.totalCount;
      } else {
        this.incidences = result;
        this.totalCount = result.length;
      }
      this.cargando = false;
    }, (err: any) => {
      console.log('Error al obtener incidencias:', err);
      this.cargando = false;
    });
  }

  onPageChange(newPage: number) {
    this.page = newPage;
    this.loadIncidences();
  }

  onPageSizeChange(newSize: number) {
    this.pageSize = +newSize;
    this.page = 1;
    this.loadIncidences();
  }
}
