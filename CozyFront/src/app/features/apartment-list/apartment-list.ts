import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Apartment } from '../../models/apartment';
import { ApartmentWorker } from '../../services/apartment-worker/apartment-worker';

@Component({
  selector: 'app-apartment-list',
  standalone: false,
  templateUrl: './apartment-list.html',
  styleUrl: './apartment-list.css'
})

export class ApartmentList implements OnInit {
  maxRooms: number = 10;
  maxBaths: number = 10;
  minRooms: number = 1;
  minBaths: number = 1;

  getRoomsOptions(): number[] {
    const min = this.minRooms;
    const max = this.maxRooms;
    return Array.from({ length: max - min + 1 }, (_, i) => i + min);
  }

  getBathsOptions(): number[] {
    const min = this.minBaths;
    const max = this.maxBaths;
    return Array.from({ length: max - min + 1 }, (_, i) => i + min);
  }
  apartments: Apartment[] = [];
  filteredApartments: Apartment[] = [];
  cargando: boolean = false;

  searchTitle: string = '';
  searchDoor: string = '';
  minArea: number = 0;
  maxArea: number = 1000;
  filterArea: number = 0;
  filterRooms: number | null = null;
  filterBaths: number | null = null;
  minPrice: number = 0;
  maxPrice: number = 10000;
  filterPriceMin: number = 0;
  filterPriceMax: number = 10000;

  // Paginación
  page = 1;
  pageSize = 10;
  totalCount = 0;

  constructor(private apartmentWorker: ApartmentWorker) { }

  ngOnInit(): void {
    this.cargando = true;
    this.loadApartments();
  }

  loadApartments() {
    const filters: any = {
      page: this.page,
      pageSize: this.pageSize,
      title: this.searchTitle || undefined,
      door: this.searchDoor || undefined,
      area: this.filterArea || undefined,
      rooms: this.filterRooms || undefined,
      baths: this.filterBaths || undefined,
      priceMin: this.filterPriceMin,
      priceMax: this.filterPriceMax
    };
    this.apartmentWorker.getApartmentsWithFilters(filters).subscribe((result: any) => {
      if (result.items && result.totalCount !== undefined) {
        this.apartments = result.items;
        this.totalCount = result.totalCount;
      } else {
        this.apartments = result;
        this.totalCount = result.length;
      }
      this.filteredApartments = this.apartments;
      this.maxRooms = Math.max(...this.apartments.map(a => a.numberOfRooms ?? 0), 1);
      this.maxBaths = Math.max(...this.apartments.map(a => a.numberOfBathrooms ?? 0), 1);
      this.maxPrice = Math.max(...this.apartments.map(a => a.price ?? 0), 10000) + 100;
      this.maxArea = Math.max(...this.apartments.map(a => a.area ?? 0), 1000);
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
}
