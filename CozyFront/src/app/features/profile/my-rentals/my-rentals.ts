import { Component, Input } from '@angular/core';
import { RentalsService } from '../../../services/rentals/rentals.service';
import { UserProfile } from '../../../models/user';
import { Rental } from '../../../models/rental';
import { UserService } from '../../../services/user/user.service';

@Component({
  selector: 'app-my-rentals',
  standalone: false,
  templateUrl: './my-rentals.html',
  styleUrl: './my-rentals.css'
})
export class MyRentals {
  // @Input() user!: UserProfile;
  @Input() user: UserProfile | null = null;

  rentals: Rental[] = [];
  vista: 'lista' | 'card' = 'lista';

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    if (this.user?.id) {
      this.loadRentals(this.user.id);
    }
  }

  loadRentals(userId: number) {
    this.userService.getUserRentals(userId).subscribe({
    // this.rentalsService.getRentalsByUserId(userId).subscribe({
      next: (data) => this.rentals = data,
      error: (err) => console.error('Error al cargar alquileres', err)
    });
  }

  toggleVista(tipo: 'lista' | 'card') {
    this.vista = tipo;
  }
}
