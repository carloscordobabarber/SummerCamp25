import { Component } from '@angular/core';
import { UserService } from '../../services/user/user.service';

@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.html',
  styleUrl: './header.css'
})
export class Header {
  constructor(private userService: UserService) {}

  logout() {
    localStorage.clear();
  }

  get isAdmin(): boolean {
    return this.userService.getRoleFromToken() === 'Admin';
  }

  get isLoggedIn(): boolean {
    return this.userService.getUserIdFromToken() !== null;
  }
}
