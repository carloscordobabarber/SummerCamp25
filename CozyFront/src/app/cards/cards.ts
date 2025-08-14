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
  selector: 'app-cards',
  standalone: false,
  templateUrl: './cards.html',
  styleUrl: './cards.css'
})
export class Cards {
  @Input() apartments: Apartment[] = [];
}
