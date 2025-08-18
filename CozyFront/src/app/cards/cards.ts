import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

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
export class Cards implements OnInit {
  apartments: Apartment[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.http.get<Apartment[]>('https://devdemoapi4.azurewebsites.net/api/apartmentclients')
      .subscribe(data => {
        this.apartments = data;
      });
  }
}
