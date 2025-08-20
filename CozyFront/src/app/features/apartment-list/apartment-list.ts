import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Apartment } from '../../models/apartment';

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
