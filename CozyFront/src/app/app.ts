import { Component, signal, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Apartment } from './cards/cards';

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
    this.http.get<Apartment[]>('assets/apartments.json').subscribe(data => {
      this.apartments = data;
    });
  }
}
