import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.html',
  styleUrl: './header.css'
})
export class Header {
  get isAdmin(): boolean {
    try {
      const user = localStorage.getItem('userRole');
      console.log('Retrieved user from localStorage:', user);
      if (!user) return false;
      const parsed = JSON.parse(user);
      var adminBoolean = Boolean(parsed.userRole);
      console.log('User role isAdmin check:', parsed.userRole, adminBoolean);
      return parsed.userRole === 'Admin';
    } catch {
      return false;
    }
  }
}
