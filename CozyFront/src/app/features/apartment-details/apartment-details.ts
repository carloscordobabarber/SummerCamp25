import { Component, OnInit, AfterViewInit } from '@angular/core';
declare var bootstrap: any;
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
  currentImageIndex: number = 0;

  getImageUrls(): string[] {
    return this.apartment?.imageUrls ?? [];
  }

  constructor(private route: ActivatedRoute, private apartmentService: ApartmentCard) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.apartmentService.getApartments().subscribe(apartments => {

      this.apartment = apartments.items.find(a => a.id === id);

    });
  }

  nextImage(): void {
    const images = this.getImageUrls();
    if (images.length > 0) {
      this.currentImageIndex = (this.currentImageIndex + 1) % images.length;
    }
  }

  prevImage(): void {
    const images = this.getImageUrls();
    if (images.length > 0) {
      this.currentImageIndex = (this.currentImageIndex - 1 + images.length) % images.length;
    }
  }

  selectImage(idx: number): void {
    this.currentImageIndex = idx;
  }

  // Manejo de error de imagen para el carrusel
  onImageError(event: Event) {
    if (event && event.target) {
      const target = event.target as HTMLImageElement;
      target.src = 'assets/CHNotFoundImage.png';
    }
  }
}