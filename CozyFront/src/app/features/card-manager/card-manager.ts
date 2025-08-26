import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApartmentCard as ApartmentCardService } from '../../services/apartment-card/apartment-card';

@Component({
  selector: 'app-card-manager',
  standalone: false,
  templateUrl: './card-manager.html',
  styleUrl: './card-manager.css'
})
export class CardManager implements OnInit {
  apartments: any[] = [];
  totalCount = 0;
  page = 1;
  pageSize = 10;

  constructor(private http: HttpClient, private apartmentCardService: ApartmentCardService) {}

  ngOnInit() {
    this.loadApartments();
  }

  loadApartments() {
    this.apartmentCardService.getApartments({
      page: this.page,
      pageSize: this.pageSize
      // Puedes añadir más filtros aquí si lo necesitas
    }).subscribe(result => {
      this.apartments = result.items ?? [];
      this.totalCount = result.totalCount ?? this.apartments.length;
    });
  }

  onPageChange(newPage: number) {
    this.page = newPage;
    this.loadApartments();
  }

  onPageSizeChange(newSize: number) {
    this.pageSize = +newSize;
    this.page = 1;
    this.loadApartments();
  }
}
