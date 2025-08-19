import { Component, signal, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Apartment } from './models/apartment';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})
export class App implements OnInit {
  protected readonly title = signal('CozyFront');
  apartments: Apartment[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    // https://devdemoapi4.azurewebsites.net/api/apartmentclients
    this.http.get<Apartment[]>('https://devdemoapi4.azurewebsites.net/api/apartmentclients').subscribe(data => {
      this.apartments = data;
    });
  }
}
