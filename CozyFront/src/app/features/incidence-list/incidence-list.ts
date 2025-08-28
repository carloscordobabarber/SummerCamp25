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

  // Filtros
  issueTypeOptions = [
    'Avería eléctrica',
    'Fuga de agua',
    'Problema de calefacción',
    'Daños estructurales',
    'Plagas',
    'Problemas de acceso',
    'Otros'
  ];
  filterIssueType: string = '';
  filterAssignedCompany: string = '';
  filterApartmentId: string = '';
  filterRentalId: string = '';
  filterTenantId: string = '';
  filterStatusId: string = '';

  constructor(private incidencesService: IncidencesService) { }

  ngOnInit(): void {
    this.cargando = true;
    this.loadIncidences();
  }

  loadIncidences() {
    const filters: any = {
      issueType: this.filterIssueType && this.filterIssueType !== '-' ? this.issueTypeOptions.indexOf(this.filterIssueType) + 1 : undefined,
      assignedCompany: this.filterAssignedCompany ? this.filterAssignedCompany : undefined,
      apartmentId: this.filterApartmentId ? this.filterApartmentId : undefined,
      rentalId: this.filterRentalId ? this.filterRentalId : undefined,
      tenantId: this.filterTenantId ? this.filterTenantId : undefined,
      statusId: this.filterStatusId ? this.filterStatusId : undefined
    };
    this.incidencesService.getIncidences(this.page, this.pageSize, filters).subscribe((result: any) => {
      let items = result.items && result.totalCount !== undefined ? result.items : result;
      this.incidences = items;
      this.totalCount = result.totalCount !== undefined ? result.totalCount : items.length;
      this.cargando = false;
    }, (err: any) => {
      console.log('Error al obtener incidencias:', err);
      this.cargando = false;
    });
  }

  applyFilters() {
    this.page = 1;
    this.loadIncidences();
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

  onAssignedCompanySearch(term: string) {
    this.filterAssignedCompany = term;
    this.applyFilters();
  }
  onApartmentIdSearch(term: string) {
    this.filterApartmentId = term;
    this.applyFilters();
  }
  onRentalIdSearch(term: string) {
    this.filterRentalId = term;
    this.applyFilters();
  }
  onTenantIdSearch(term: string) {
    this.filterTenantId = term;
    this.applyFilters();
  }
  onStatusIdSearch(term: string) {
    this.filterStatusId = term;
    this.applyFilters();
  }

  getIssueTypeText(issueType: number): string {
    switch (issueType) {
      case 1: return 'Avería eléctrica';
      case 2: return 'Fuga de agua';
      case 3: return 'Problema de calefacción';
      case 4: return 'Daños estructurales';
      case 5: return 'Plagas';
      case 6: return 'Problemas de acceso';
      default: return 'Otros';
    }
  }
}
