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
  apartments: Apartment[] = [];
  filteredApartments: Apartment[] = [];
  cargando: boolean = false;

  searchTitle: string = '';
  searchDoor: string = '';
  searchArea: string = '';
  filterRooms: number = 0;
  filterBaths: number = 0;
  filterPrice: number = 0;
  minRooms: number = 0;
  maxRooms: number = 10;
  minBaths: number = 0;
  maxBaths: number = 10;
  minPrice: number = 0;
  maxPrice: number = 10000;

  constructor(private apartmentWorker: ApartmentWorker) {}

  // ngOnInit() {
  //   this.http.get<Apartment[]>('https://devdemoapi4.azurewebsites.net/api/apartmentworkers')
  //     .subscribe(data => {
  //       this.apartments = data;
  //     });
  // }

  ngOnInit(): void {
    this.cargando = true;
    this.apartmentWorker.getApartments().subscribe({
      next: (data) => {
        this.apartments = data;
        this.filteredApartments = data;
        // Calcular valores máximos para sliders
        this.maxRooms = Math.max(...data.map(a => a.numberOfRooms ?? 0), 10);
        this.maxBaths = Math.max(...data.map(a => a.numberOfBathrooms ?? 0), 10);
        this.maxPrice = Math.max(...data.map(a => a.price ?? 0), 10000);
        this.filterRooms = this.minRooms;
        this.filterBaths = this.minBaths;
        this.filterPrice = this.minPrice;
        this.cargando = false;
      },
      error: (err) => {
        console.log('Error al obtener datos:', err);
        this.cargando = false;
      },
      complete: () => {
        console.log('Datos recibidos:', this.apartments);
      }
    });
  }

  applyFilters() {
    this.filteredApartments = this.apartments.filter(apt => {
      const matchTitle = !this.searchTitle || apt.code?.toLowerCase().includes(this.searchTitle.toLowerCase());
      const matchDoor = !this.searchDoor || apt.door?.toLowerCase().includes(this.searchDoor.toLowerCase());
      const matchArea = !this.searchArea || apt.area?.toString().includes(this.searchArea);
      const matchRooms = !this.filterRooms || apt.numberOfRooms === this.filterRooms;
      const matchBaths = !this.filterBaths || apt.numberOfBathrooms === this.filterBaths;
      const matchPrice = !this.filterPrice || apt.price <= this.filterPrice;
      return matchTitle && matchDoor && matchArea && matchRooms && matchBaths && matchPrice;
    });
  }
}
