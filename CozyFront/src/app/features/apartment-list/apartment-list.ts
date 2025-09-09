
import { Component, OnInit } from '@angular/core';
import { Apartment } from '../../models/apartment';
import { ApartmentFilters } from '../../models/apartment-filters';

type ApartmentApiResult = { items: Apartment[]; totalCount: number } | Apartment[];
import { ApartmentWorker } from '../../services/apartment-worker/apartment-worker';

@Component({
  selector: 'app-apartment-list',
  standalone: false,
  templateUrl: './apartment-list.html',
  styleUrl: './apartment-list.css'
})

export class ApartmentList implements OnInit {
  roomsOptions: number[] = [];
  bathsOptions: number[] = [];
  allApartments: Apartment[] = [];

  getRoomsOptions(): number[] {
    return this.roomsOptions;
  }

  getBathsOptions(): number[] {
    return this.bathsOptions;
  }
  apartments: Apartment[] = [];
  filteredApartments: Apartment[] = [];
  cargando: boolean = false;

  searchStreet: string = '';
  searchCode: string = '';
  minArea: number = 0;
  maxArea: number = 250;
  filterArea: number = 0;
  filterRooms: number | null = null;
  filterBaths: number | null = null;
  minPrice: number = 0;
  maxPrice: number = 5000;
  filterPriceMin: number = 0;
  filterPriceMax: number = 5000;

  // Paginación
  page = 1;
  pageSize = 10;
  totalCount = 0;

  constructor(private apartmentWorker: ApartmentWorker) { }

  ngOnInit(): void {
    this.cargando = true;
    // Cargar todos los apartamentos para los filtros globales
    this.apartmentWorker.getApartmentsWithFilters({ page: 1, pageSize: 10000 }).subscribe((result: ApartmentApiResult) => {
      let all: Apartment[];
      if (Array.isArray(result)) {
        all = result;
      } else if ('items' in result) {
        all = result.items;
      } else {
        all = [];
      }
      this.allApartments = all;
      this.roomsOptions = Array.from(new Set(all.map((a: Apartment) => Number(a.numberOfRooms)).filter((n: number) => !isNaN(n)))).sort((a: number, b: number) => a - b);
      this.bathsOptions = Array.from(new Set(all.map((a: Apartment) => Number(a.numberOfBathrooms)).filter((n: number) => !isNaN(n)))).sort((a: number, b: number) => a - b);
      // Ahora cargar la página actual
      this.loadApartments();
    });
  }

  loadApartments() {
    const filters: ApartmentFilters = {
      page: this.page,
      pageSize: this.pageSize,
      minPrice: this.filterPriceMin,
      maxPrice: this.filterPriceMax,
      area: this.filterArea || undefined,
      numberOfRooms: this.filterRooms || undefined,
      numberOfBathrooms: this.filterBaths || undefined,
      street: this.searchStreet || undefined,
      code: this.searchCode || undefined
    };
    this.apartmentWorker.getApartmentsWithFilters(filters).subscribe((result: ApartmentApiResult) => {
      if (Array.isArray(result)) {
        this.apartments = result;
        this.totalCount = result.length;
      } else if ('items' in result && 'totalCount' in result) {
        this.apartments = result.items;
        this.totalCount = result.totalCount;
      } else {
        this.apartments = [];
        this.totalCount = 0;
      }
      this.filteredApartments = this.apartments;
      // Las opciones de los filtros ya se calculan en ngOnInit con todos los apartamentos
      this.maxPrice = Math.max(...this.apartments.map(a => a.price ?? 0), 5000);
      this.maxArea = Math.max(...this.apartments.map(a => a.area ?? 0), 250);
      this.cargando = false;
    }, (err: any) => {
      console.log('Error al obtener datos:', err);
      this.cargando = false;
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

  applyFilters() {
    this.page = 1;
    this.loadApartments();
  }

  onCodeSearch(term: string) {
    this.searchCode = term;
    this.applyFilters();
  }

  onStreetSearch(term: string) {
    this.searchStreet = term;
    this.applyFilters();
  }
}
