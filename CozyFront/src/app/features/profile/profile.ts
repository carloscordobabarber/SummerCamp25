import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../services/user/user.service';
import { UserProfile } from '../../models/user';

@Component({
  selector: 'app-profile',
  standalone: false,
  templateUrl: './profile.html',
  styleUrl: './profile.css'
})
export class Profile implements OnInit {
  showProfileDropdown = false;
  loggedUser: UserProfile | null = null;
  selectedSection: string = 'details'; // Por defecto muestra 'Mi Perfil'

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    // Llamar a la API para datos actualizados usando JWT
    this.userService.getUser().subscribe({
      next: (user) => {
        this.loggedUser = user;
        // Actualizar el localStorage con la versión más reciente
        localStorage.setItem('user', JSON.stringify(user));
      },
      error: (err) => {
        console.error('Error actualizando el usuario desde API:', err);
      }
    });
  }

  selectSection(section: string): void {
    this.selectedSection = section;
    if (section !== 'details') {
      this.showProfileDropdown = false;
    }
  }

  toggleProfileDropdown(): void {
    if (this.selectedSection !== 'details') {
      this.selectedSection = 'details';
      this.showProfileDropdown = true;
    } else {
      this.showProfileDropdown = !this.showProfileDropdown;
    }
  }
}
