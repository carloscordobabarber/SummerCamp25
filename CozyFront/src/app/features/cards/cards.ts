import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApartmentClientsService } from '../../services/apartment-client/apartment-client';
import { Apartment } from '../../models/apartment';

@Component({
  selector: 'app-cards',
  standalone: false,
  templateUrl: './cards.html',
  styleUrl: './cards.css'
})
export class Cards implements OnInit {
  apartments: Apartment[] = [];

  constructor(private apartmentClient: ApartmentClientsService) {}

  // ngOnInit() {
  //   this.apartmentClient.getApartments().subscribe(data => {
  //     this.apartments = data;
  //   });
  // }

  ngOnInit(): void {
    this.apartmentClient.getApartments().subscribe({
      next: (res) => {
        this.apartments = res;
        console.log('Datos recibidos:', this.apartments);
      },
      error: (err) => {
        console.error('Error al obtener datos:', err);
      }
    });
  }
}
