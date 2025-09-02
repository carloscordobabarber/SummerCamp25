import { AfterViewInit, ElementRef, ViewChild } from '@angular/core';
declare var bootstrap: any;
import { Component, Input } from '@angular/core';
import { UserProfile } from '../../../models/user';
import { UserRentalsService } from '../../../services/user/user-rentals.service';
import { UserRental } from '../../../models/user-rental';
import { ApartmentCardDto } from '../../../services/apartment-card/apartment-card';

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
  apartments: ApartmentCardDto[] = [];
  vista: 'lista' | 'card' = 'lista';

  constructor(private userRentalsService: UserRentalsService) {}
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
    } else {
      console.error('No se encontró el id de usuario en localStorage');
    }
  }

  loadRentals(userId: number) {
    this.userRentalsService.getRentalsByUserId(userId).subscribe({
      next: (data) => {
        this.rentals = data;
        // Mapear cada UserRental a un objeto ApartmentCardDto compatible
        this.apartments = data.map(r => ({
          id: r.apartmentId,
          code: r.apartmentCode,
          door: r.apartmentDoor,
          floor: r.apartmentFloor?.toString() ?? '',
          price: r.apartmentPrice,
          area: 0, // No disponible en UserRental
          numberOfRooms: 0, // No disponible en UserRental
          numberOfBathrooms: 0, // No disponible en UserRental
          buildingId: 0, // No disponible en UserRental
          hasLift: false, // No disponible en UserRental
          hasGarage: false, // No disponible en UserRental
          isAvailable: true, // No disponible en UserRental
          streetName: r.streetName,
          districtId: 0, // No disponible en UserRental
          districtName: r.districtName,
          imageUrls: [] // No disponible en UserRental
        }));
      },
      error: (err) => console.error('Error al cargar alquileres', err)
    });
  }

  toggleVista(tipo: 'lista' | 'card') {
    this.vista = tipo;
  }
}
