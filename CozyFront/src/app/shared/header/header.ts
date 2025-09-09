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
    const token = localStorage.getItem('token');
    if (!token) return false;
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return !!payload.sub;
    } catch {
      return false;
    }
  }
}
