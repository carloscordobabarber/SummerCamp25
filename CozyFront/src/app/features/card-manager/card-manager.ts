
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

interface ApartmentCard {
  id: number;
  code: string;
  door: string;
  floor: number;
  price: number;
  area: number;
  numberOfRooms: number;
  numberOfBathrooms: number;
  buildingId: number;
  hasLift: boolean;
  hasGarage: boolean;
  streetName: string;
  districtId: number;
  districtName: string;
  imageUrls: string[];
  isAvailable?: boolean;
}

@Component({
  selector: 'app-card-manager',
  standalone: false,
  templateUrl: './card-manager.html',
  styleUrl: './card-manager.css'
})
export class CardManager implements OnInit {
  apartments: ApartmentCard[] = [];
  totalCount = 0;
  page = 1;
  pageSize = 10;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.loadApartments();
  }

  loadApartments() {
    this.http.get<{ totalCount: number, items: ApartmentCard[] }>(
      `/api/ApartmentCard?page=${this.page}&pageSize=${this.pageSize}`
    ).subscribe(result => {
      this.apartments = result.items;
      this.totalCount = result.totalCount;
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
}
