import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Apartment } from '../../models/apartment';
import { ApartmentCard } from '../../services/apartment-card/apartment-card';
import { RentalsService, RentalDto } from '../../services/rentals/rentals.service';

@Component({
  selector: 'app-apartment-details',
  standalone: false,
  templateUrl: './apartment-details.html',
  styleUrl: './apartment-details.css'
})
export class ApartmentDetails implements OnInit {
  apartment?: Apartment;
  currentImageIndex: number = 0;
  userId: number | null = null;
  rentMessage: string = '';
  isRenting: boolean = false;
  startDate: string = '';
  todayString: string = '';

  getImageUrls(): string[] {
    return this.apartment?.imageUrls ?? [];
  }

  constructor(
    private route: ActivatedRoute,
    private apartmentService: ApartmentCard,
    private rentalsService: RentalsService
  ) { }

  ngOnInit(): void {
    // Limitar selector de fecha a solo futuras
    const today = new Date();
    this.todayString = today.toISOString().split('T')[0];
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.apartmentService.getApartments().subscribe(apartments => {
      this.apartment = apartments.items.find(a => a.id === id);
    });
    // Detectar usuario en localStorage
    const user = localStorage.getItem('user');
    if (user) {
      try {
        const userObj = JSON.parse(user);
        this.userId = userObj.id || null;
      } catch {
        this.userId = null;
      }
    } else {
      this.userId = null;
    }
  }
  onRentNow(): void {
    if (!this.userId || !this.apartment) {
      this.rentMessage = 'Registrese para poder Alquilar';
      return;
    }
    if (!this.startDate) {
      this.rentMessage = 'Seleccione una fecha de inicio.';
      return;
    }
    this.isRenting = true;
    const start = new Date(this.startDate);
    const end = new Date(start);
    end.setFullYear(end.getFullYear() + 1);
    // Comprobar conflicto antes del POST
    this.rentalsService.checkDate(this.apartment.id, start.toISOString()).subscribe(resp => {
      if (resp.exists) {
        this.rentMessage = resp.message || 'Ya existe un alquiler para este apartamento en la fecha seleccionada.';
        this.isRenting = false;
        return;
      }
      const rental: Partial<RentalDto> = {
        userId: this.userId!,
        apartmentId: this.apartment!.id,
        startDate: start.toISOString(),
        endDate: end.toISOString(),
        statusId: 1
      };
      this.rentalsService.createRental(rental as RentalDto).subscribe({
        next: () => {
          this.rentMessage = '¡Alquiler realizado con éxito!';
          this.isRenting = false;
        },
        error: () => {
          this.rentMessage = 'Error al realizar el alquiler.';
          this.isRenting = false;
        }
      });
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
// ...