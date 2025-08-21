import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Apartment } from '../../models/apartment';
import { ApartmentClientsService } from '../../services/apartment-client/apartment-client';

@Component({
  selector: 'app-apartment-list',
  standalone: false,
  templateUrl: './apartment-list.html',
  styleUrl: './apartment-list.css'
})
export class ApartmentList implements OnInit {
  apartments: Apartment[] = [];
  cargando: boolean = false;

  constructor(private apartmentClientService: ApartmentClientsService) {}

  // ngOnInit() {
  //   this.http.get<Apartment[]>('https://devdemoapi4.azurewebsites.net/api/apartmentworkers')
  //     .subscribe(data => {
  //       this.apartments = data;
  //     });
  // }

  ngOnInit(): void {
    this.cargando = true;
    this.apartmentClientService.getApartments().subscribe({
      next: (data) => {
        this.apartments = data; 
        console.log('Datos recibidos:', this.apartments);
        this.cargando = false;
      },
      error: (err) => {
        console.log('Error al obtener datos:', err);
        this.cargando = false;
      }
    });
  }
}
