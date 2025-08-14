import { Component, Input } from '@angular/core';

export interface Apartment {
  id: number;
  title: string;
  address: string;
  price: number;
  rooms: number;
  bathrooms: number;
  area: number;
  image: string;
  available: boolean;
}

@Component({
  selector: 'app-apartment-list',
  standalone: false,
  templateUrl: './apartment-list.html',
  styleUrl: './apartment-list.css'
})
export class ApartmentList {
  @Input() apartments: Apartment[] = [];
}
