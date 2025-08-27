import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApartmentCard as ApartmentCardService } from '../../services/apartment-card/apartment-card';

@Component({
  selector: 'app-card-manager',
  standalone: false,
  templateUrl: './card-manager.html',
  styleUrl: './card-manager.css'
})
export class CardManager implements OnInit {
  apartments: any[] = [];
  totalCount = 0;
  page = 1;
  pageSize = 10;

  // Filtros
  area: number = 20;
  price: number = 200;
  numberOfRooms: number = 1;
  numberOfBathrooms: number = 1;
  hasLift?: boolean;
  hasGarage?: boolean;
  searchTerm: string = '';

  constructor(private http: HttpClient, private apartmentCardService: ApartmentCardService) {}

  ngOnInit() {
    this.loadApartments();
  }

  loadApartments() {
    // Solo enviar los filtros si el usuario los ha cambiado respecto al default
    const params: any = {
      page: this.page,
      pageSize: this.pageSize
    };
    if (this.area !== 20) params.area = this.area;
    if (this.price !== 200) params.price = this.price;
    if (this.numberOfRooms !== 1) params.numberOfRooms = this.numberOfRooms;
    if (this.numberOfBathrooms !== 1) params.numberOfBathrooms = this.numberOfBathrooms;
    if (this.hasLift !== undefined) params.hasLift = this.hasLift;
    if (this.hasGarage !== undefined) params.hasGarage = this.hasGarage;
    // Puedes agregar más filtros aquí
    // if (this.searchTerm) params.searchTerm = this.searchTerm;

    this.apartmentCardService.getApartments(params).subscribe(result => {
      this.apartments = result.items ?? [];
      this.totalCount = result.totalCount ?? this.apartments.length;
    });
  }

  onPageChange(newPage: number) {
    this.page = newPage;
    this.loadApartments();
  }

  onPageSizeChange(newSize: number) {
    this.pageSize = +newSize;
    this.page = 1;
    this.loadApartments();
  }

  onFilterChange() {
    this.page = 1;
    this.loadApartments();
  }

  onSearch(term: string) {
    this.searchTerm = term;
    this.page = 1;
    // Si la API soporta búsqueda por texto, descomenta la línea en loadApartments
    this.loadApartments();
  }
}
