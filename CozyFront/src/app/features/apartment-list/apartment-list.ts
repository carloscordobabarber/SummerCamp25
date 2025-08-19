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
  selector: 'app-apartment-list',
  standalone: false,
  templateUrl: './apartment-list.html',
  styleUrl: './apartment-list.css'
})
export class ApartmentList implements OnInit {
  apartments: Apartment[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.http.get<Apartment[]>('https://devdemoapi4.azurewebsites.net/api/apartmentworkers')
      .subscribe(data => {
        this.apartments = data;
      });
  }
}
