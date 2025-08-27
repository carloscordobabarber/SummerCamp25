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
  filterPrice: number = 0;
  minPrice: number = 0;
  maxPrice: number = 10000;

  // Paginación
  page = 1;
  pageSize = 10;
  totalCount = 0;

  constructor(private apartmentWorker: ApartmentWorker) { }

  // ngOnInit() {
  //   this.http.get<Apartment[]>('https://devdemoapi4.azurewebsites.net/api/apartmentworkers')
  //     .subscribe(data => {
  //       this.apartments = data;
  //     });
  // }

  ngOnInit(): void {
    this.cargando = true;
    this.loadApartments();
  }

  loadApartments() {
    this.apartmentWorker.getApartments(this.page, this.pageSize).subscribe((result: any) => {
      if (result.items && result.totalCount !== undefined) {
        this.apartments = result.items;
        this.totalCount = result.totalCount;
      } else {
        this.apartments = result;
        this.totalCount = result.length;
      }
      this.filteredApartments = this.apartments;
      this.maxRooms = Math.max(...this.apartments.map(a => a.numberOfRooms ?? 0));
      this.maxBaths = Math.max(...this.apartments.map(a => a.numberOfBathrooms ?? 0));
      this.maxPrice = Math.max(...this.apartments.map(a => a.price ?? 0)) + 100;
      this.maxArea = Math.max(...this.apartments.map(a => a.area ?? 0));
      this.filterRooms = null;
      this.filterBaths = null;
      this.filterPrice = this.minPrice;
      this.filterArea = this.minArea;
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
    this.filteredApartments = this.apartments.filter(apt => {
      const matchTitle = !this.searchTitle || apt.code?.toLowerCase().includes(this.searchTitle.toLowerCase());
      const matchDoor = !this.searchDoor || apt.door?.toLowerCase().includes(this.searchDoor.toLowerCase());
      const matchArea = !this.filterArea || apt.area >= this.filterArea;
      const matchRooms = this.filterRooms == null || apt.numberOfRooms === this.filterRooms;
      const matchBaths = this.filterBaths == null || apt.numberOfBathrooms === this.filterBaths;
      const matchPrice = !this.filterPrice || apt.price <= this.filterPrice;
      return matchTitle && matchDoor && matchArea && matchRooms && matchBaths && matchPrice;
    });
  }
}
