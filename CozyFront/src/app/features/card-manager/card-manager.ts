
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

  // Filtros de rango para área y precio
  area: number = 20;
  minPrice: number = 200;
  maxPrice: number = 5000;

  // se consulta a la API con los filtros actuales

  // Filtros de habitaciones y baños (usados por search-bar)
  numberOfRooms: number | null = null;
  numberOfBathrooms: number | null = null;
  name: string = '';

  hasLift?: boolean;
  hasGarage?: boolean;

  constructor(private http: HttpClient, private apartmentCardService: ApartmentCardService) {}

  ngOnInit() {
    this.loadApartments();
  }

  resetFilters() {
    this.area = 20;
    this.minPrice = 200;
    this.maxPrice = 5000;
    this.numberOfRooms = null;
    this.numberOfBathrooms = null;
    this.hasLift = undefined;
    this.hasGarage = undefined;
    this.name = '';
    this.page = 1;
    this.loadApartments();
  }

  loadApartments() {
    const params: any = {
      page: this.page,
      pageSize: this.pageSize
    };
    // Filtro de área (valor único)
    if (this.area !== 20) params.area = this.area;
    // Filtros de rango para precio
    if (this.minPrice !== 200) params.minPrice = this.minPrice;
    if (this.maxPrice !== 5000) params.maxPrice = this.maxPrice;
    // Filtros de habitaciones y baños
    if (this.numberOfRooms !== null) params.numberOfRooms = this.numberOfRooms;
    if (this.numberOfBathrooms !== null) params.numberOfBathrooms = this.numberOfBathrooms;
    // Filtro de nombre
    if (this.name && this.name.trim().length > 0) params.name = this.name.trim();
    // Otros filtros
    if (this.hasLift !== undefined) params.hasLift = this.hasLift;
    if (this.hasGarage !== undefined) params.hasGarage = this.hasGarage;

    this.apartmentCardService.getApartments(params).subscribe(result => {
      this.apartments = result.items ?? [];
      this.totalCount = result.totalCount ?? this.apartments.length;
    });
   
  }

  onPageChange(newPage: number) {
    this.page = newPage;
    this.loadApartments();
     console.log(this.apartments);
  }

  onPageSizeChange(newSize: number) {
    this.pageSize = +newSize;
    this.page = 1;
    this.loadApartments();
  }

  onAreaChange(val: number) {
    this.area = val;
    this.page = 1;
    this.loadApartments();
  }

  onPriceRangeChange(range: {min: number, max: number}) {
    this.minPrice = range.min;
    this.maxPrice = range.max;
    this.page = 1;
    this.loadApartments();
  }

  onRoomsSearch(term: string) {
    const value = parseInt(term, 10);
    this.numberOfRooms = isNaN(value) ? null : value;
    this.page = 1;
    this.loadApartments();
  }

  onBathroomsSearch(term: string) {
    const value = parseInt(term, 10);
    this.numberOfBathrooms = isNaN(value) ? null : value;
    this.page = 1;
    this.loadApartments();
  }

  onNameSearch(term: string) {
    this.name = term;
    this.page = 1;
    this.loadApartments();
  }

}
