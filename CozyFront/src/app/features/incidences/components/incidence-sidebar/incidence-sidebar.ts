import { Component, OnInit } from '@angular/core';
import { DatePipe, CommonModule } from '@angular/common';
import { IncidencesService } from '../../../../services/incidences/incidences.service';
import { UserService } from '../../../../services/user/user.service';
import { Incidence } from '../../../../models/incidence';

@Component({
  selector: 'app-incidence-sidebar',
  templateUrl: './incidence-sidebar.html',
  styleUrls: ['./incidence-sidebar.css'],
  standalone: true,
  imports: [CommonModule, DatePipe]
})
export class IncidenceSidebar implements OnInit {
  incidences: Incidence[] = [];
  incidenceTypes = [
    'Avería eléctrica',
    'Fuga de agua',
    'Problema de calefacción',
    'Daños estructurales',
    'Plagas',
    'Problemas de acceso',
    'Otros'
  ];

  constructor(private incidencesService: IncidencesService, private userService: UserService) {}

  ngOnInit() {
    const userId = this.userService.getUserIdFromToken();
    if (userId !== null) {
      this.getUserIncidences(userId.toString());
    }
  }

  getUserIncidences(userId: string) {
    this.incidencesService.getIncidences(1, 10000, { tenantId: userId }).subscribe((res: any) => {
      this.incidences = res.items;
      const event = new CustomEvent('incidencesLoaded', { detail: this.incidences });
      window.dispatchEvent(event);
    });
  }

  getTypeName(type: number): string {
    return this.incidenceTypes[type] ?? 'Desconocido';
  }

  selected: Incidence | null = null;

  selectIncidence(inc: Incidence) {
    this.selected = inc;
    const event = new CustomEvent('incidenceSelected', { detail: inc });
    window.dispatchEvent(event);
  }
}
