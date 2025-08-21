import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Apartment } from '../../models/apartment';

@Component({
  selector: 'app-card-manager',
  standalone: false,
  templateUrl: './card-manager.html',
  styleUrl: './card-manager.css'
})
export class CardManager implements OnInit {
  apartments: Apartment[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.http.get<Apartment[]>('https://devdemoapi4.azurewebsites.net/api/apartmentclients').subscribe(data => {
      this.apartments = data;
    });
  }
}
