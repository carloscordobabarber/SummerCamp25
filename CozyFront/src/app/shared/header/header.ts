import { Component } from '@angular/core';
import { UserService } from '../../services/user/user.service';

@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.html',
  styleUrl: './header.css'
})
export class Header {
  isMenuOpen = false;
  
  constructor(private userService: UserService) { }

  toggleMenu(): void {
    this.isMenuOpen = !this.isMenuOpen;
  }

  logout() {
    localStorage.clear();
    this.isMenuOpen = false; // Cierra menú al hacer logout
  }

  get isAdmin(): boolean {
    return this.userService.getRoleFromToken() === 'Admin';
  }

  get isLoggedIn(): boolean {
    return this.userService.getUserIdFromToken() !== null;
  }
}
