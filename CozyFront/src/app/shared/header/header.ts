import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.html',
  styleUrl: './header.css'
})
export class Header {
  logout() {
    localStorage.clear();
  }
  get isAdmin(): boolean {
    try {
      const user = localStorage.getItem('userRole');
      if (!user) return false;
      return user === 'Admin';
    } catch {
      return false;
    }
  }
  get isLoggedIn(): boolean {
    try {
      const user = localStorage.getItem('userId');
      if (!user) return false;
      return true;
    } catch {
      return false;
    }
  }
}
