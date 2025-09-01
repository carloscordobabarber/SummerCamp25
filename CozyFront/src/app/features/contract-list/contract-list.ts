import { Component, OnInit } from '@angular/core';
import { RentalsService } from '../../services/rentals/rentals.service';
import { Rental } from '../../models/rental';

@Component({
  selector: 'app-contract-list',
  standalone: false,
  templateUrl: './contract-list.html',
  styleUrl: './contract-list.css'
})
export class ContractList implements OnInit {
  rentals: Rental[] = [];
  cargando: boolean = false;

  // Filtros
  filterUserId: string = '';
  filterApartmentId: string = '';
  filterStatusId: string = '';
  filterStartDate: string = '';
  filterEndDate: string = '';

  // Paginación
  page = 1;
  pageSize = 10;
  totalCount = 0;

  constructor(private rentalsService: RentalsService) {}

  ngOnInit(): void {
    this.loadRentals();
  }

  loadRentals() {
    this.cargando = true;
    this.rentalsService.getRentalsWithFilters({
      userId: this.filterUserId,
      apartmentId: this.filterApartmentId,
      statusId: this.filterStatusId,
      startDate: this.filterStartDate,
      endDate: this.filterEndDate,
      page: this.page,
      pageSize: this.pageSize
    }).subscribe((result: { items: Rental[]; totalCount: number }) => {
      this.rentals = result.items;
      this.totalCount = result.totalCount;
      this.cargando = false;
    }, err => {
      this.cargando = false;
    });
  }

  onUserIdSearch(term: string) {
    this.filterUserId = term;
    this.page = 1;
    this.loadRentals();
  }
  onApartmentIdSearch(term: string) {
    this.filterApartmentId = term;
    this.page = 1;
    this.loadRentals();
  }
  onStatusIdSearch(term: string) {
    this.filterStatusId = term;
    this.page = 1;
    this.loadRentals();
  }
  onDateRangeChange(range: { startDate: string, endDate: string }) {
    this.filterStartDate = range.startDate;
    this.filterEndDate = range.endDate;
    this.page = 1;
    this.loadRentals();
  }
  onPageChange(newPage: number) {
    this.page = newPage;
    this.loadRentals();
  }
  onPageSizeChange(newSize: number) {
    this.pageSize = +newSize;
    this.page = 1;
    this.loadRentals();
  }
}
