import { Component, Input } from '@angular/core';
import { RentalsService } from '../../../services/rentals/rentals.service';
import { UserProfile } from '../../../models/user';
import { UserRental } from '../../../models/user-rental';
import { UserService } from '../../../services/user/user.service';
import { UserRentalsService } from '../../../services/user/user-rentals.service';

@Component({
  selector: 'app-my-rentals',
  standalone: false,
  templateUrl: './my-rentals.html',
  styleUrl: './my-rentals.css'
})
export class MyRentals {
  @Input() user!: UserProfile;
  
  rentals: UserRental[] = [];
  vista: 'lista' | 'card' = 'lista';

  constructor(private userService: UserRentalsService) {}

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
    this.userService.getRentalsByUserId(userId).subscribe({
      next: (data) => this.rentals = data,
      error: (err) => console.error('Error al cargar alquileres', err)
    });
  }

  toggleVista(tipo: 'lista' | 'card') {
    this.vista = tipo;
  }
}
