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

  constructor(private apartmentClientService: ApartmentClientsService) {}

  // ngOnInit() {
  //   this.apartmentClientService.getApartments().subscribe(data => {
  //     this.apartments = data;
  //   });
  // }

  ngOnInit(): void {
    this.apartmentClientService.getApartments().subscribe({
      next: (data) => {
        this.apartments = data;
        console.log('Datos recibidos:', this.apartments);
      },
      error: (err) => {
        console.error('Error al obtener datos:', err);
      }
    });
  }
}
