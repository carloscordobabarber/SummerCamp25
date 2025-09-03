import { AfterViewInit, ElementRef, ViewChild } from '@angular/core';
declare var bootstrap: any;
import { Component, Input } from '@angular/core';
import { UserProfile } from '../../../models/user';
import { ApartmentCardClientService } from '../../../services/apartment-card-client/apartment-card-client.service';
import { ApartmentCard } from '../../../models/apartment-card';
import { UserRentalsService } from '../../../services/user/user-rentals.service';
import { UserRental } from '../../../models/user-rental';

@Component({
  selector: 'app-my-rentals',
  standalone: false,
  templateUrl: './my-rentals.html',
  styleUrl: './my-rentals.css'
})
export class MyRentals implements AfterViewInit {
  @ViewChild('paymentModal') paymentModal!: ElementRef;
  selectedRental: UserRental | null = null;
  private modalInstance: any;
  @Input() user!: UserProfile;
  rentals: UserRental[] = [];
  apartments: ApartmentCard[] = [];
  vista: 'lista' | 'card' = 'lista';

  constructor(
    private userRentalsService: UserRentalsService,
    private apartmentCardClientService: ApartmentCardClientService
  ) {}
  ngAfterViewInit() {
    this.modalInstance = new bootstrap.Modal(this.paymentModal.nativeElement);
    this.paymentModal.nativeElement.addEventListener('hidden.bs.modal', () => {
      this.selectedRental = null;
    });
  }
  openPaymentForm(rental: UserRental) {
    this.selectedRental = { ...rental };
    this.modalInstance.show();
    setTimeout(() => {
      const modalElement = this.paymentModal.nativeElement as HTMLElement;
      const focusable = modalElement.querySelector('button, [href], input, select, textarea, [tabindex]:not([tabindex="-1"])') as HTMLElement;
      if (focusable) {
        focusable.focus();
      } else {
        modalElement.focus();
      }
    }, 100);
  }

  closePaymentForm() {
    this.modalInstance.hide();
  }

  ngOnInit(): void {
    const userIdStr = localStorage.getItem('userId');
    const userId = userIdStr ? parseInt(userIdStr, 10) : null;
    if (userId) {
      this.loadRentals(userId);
      this.loadApartments(userId);
    } else {
      console.error('No se encontró el id de usuario en localStorage');
    }
  }

  loadRentals(userId: number) {
    this.userRentalsService.getRentalsByUserId(userId).subscribe({
      next: (rentals) => {
        this.rentals = rentals;
      },
      error: (err) => console.error('Error al cargar alquileres', err)
    });
  }

  loadApartments(userId: number) {
    this.apartmentCardClientService.getUserApartments(userId).subscribe({
      next: (apartments) => {
        this.apartments = apartments;
      },
      error: (err) => console.error('Error al cargar apartamentos', err)
    });
  }

  toggleVista(tipo: 'lista' | 'card') {
    this.vista = tipo;
  }
}
