import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Apartment } from '../../models/apartment';
import { ApartmentCard } from '../../services/apartment-card/apartment-card';

@Component({
  selector: 'app-apartment-details',
  standalone: false,
  templateUrl: './apartment-details.html',
  styleUrl: './apartment-details.css'
})
export class ApartmentDetails implements OnInit {
  apartment?: Apartment;

  getImageUrl(): string | null {
    if (this.apartment && this.apartment.imageUrls && this.apartment.imageUrls.length > 0) {
      return this.apartment.imageUrls[0];
    }
    return null;
  }

  constructor(private route: ActivatedRoute, private apartmentService: ApartmentCard) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.apartmentService.getApartments().subscribe(apartments => {
      this.apartment = apartments.items.find(a => a.id === id);
    });
    console.log('Datos apartamento:', this.apartment);
    console.log('Id apartamento:', this.apartment?.id);
  }
}
