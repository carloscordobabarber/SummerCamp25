import { Component, Input } from '@angular/core';
import { Apartment } from '../../models/apartment';

@Component({
  selector: 'app-cards',
  standalone: false,
  templateUrl: './cards.html',
  styleUrl: './cards.css'
})
export class Cards {
  @Input() apartment!: { isAvailable?: boolean, [key: string]: any };
  currentImageIndex = 0;

  get images(): string[] {
    if (this.apartment?.['imageUrls'] && this.apartment['imageUrls'].length > 0) {
      return this.apartment['imageUrls'];
    }
    return ['assets/CHNotFoundImage.png'];
  }

  prevImage() {
    if (this.images.length > 1) {
      this.currentImageIndex = (this.currentImageIndex - 1 + this.images.length) % this.images.length;
    }
  }

  nextImage() {
    if (this.images.length > 1) {
      this.currentImageIndex = (this.currentImageIndex + 1) % this.images.length;
    }
  }

  get direccion(): string {
    const d = this.apartment;
    return `${d['districtName'] || ''} ${d['streetName'] || ''} Planta ${d['floor']}`.trim();
  }

  get disponible(): boolean {
    // Si isAvailable es undefined, consideramos true (disponible)
    return this.apartment.isAvailable !== false;
  }
}
