import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../../services/user/user.service';
import { UserProfile } from '../../../models/user';

@Component({
  selector: 'app-profile-details',
  standalone: false,
  templateUrl: './profile-details.html',
  styleUrl: './profile-details.css'
})
export class ProfileDetails implements OnInit {
  @Input() user: any;

  userForm!: FormGroup;
  // user: UserProfile | null = null;
  userName: string;

  get isFormReadyToSubmit(): boolean {
    return this.userForm && this.userForm.valid && !this.userForm.pristine;
  }

  constructor(private userService: UserService, private fb: FormBuilder) {
    this.userName = "";
  }

  ngOnInit(): void {
    this.userForm = this.fb.group({
      documentType: ['', Validators.required],
      documentNumber: ['', [Validators.required, Validators.minLength(5)]],
      name: ['', [Validators.required, Validators.minLength(3)]],
      lastName: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      birthDate: ['', Validators.required],
      phone: ['', [Validators.pattern(/^((\+\d{7,15})|(\d{9,15}))$/)]], // acepta internacional (+123456789) o nacional (123456789)
    });

    this.userService.getUser().subscribe({
      next: (userData) => {
        this.user = userData;
        this.userName = `${userData.name}`;
        console.log('birthDate type:', typeof userData.birthDate, userData.birthDate);
        this.patchUserForm(userData);
      },
      error: (err) => {
        console.error('Error al obtener usuario', err);
      }
    });
  }

  private patchUserForm(user: UserProfile) {
    this.userForm.patchValue({
      documentType: this.formatDocType(user.documentType),
      documentNumber: user.documentNumber,
      name: user.name,
      lastName: user.lastName,
      email: user.email,
      birthDate: this.formatDateForInput(user.birthDate),  // aquí hacemos la conversión
      phone: user.phone
    });
  this.userForm.markAsPristine();
  }

  private formatDocType(userDocType: string): string {
    return ['dni', 'nie'].includes(userDocType) ? userDocType.toUpperCase() : userDocType;
  }
  
  private formatDateForInput(date: string | Date | null): string {
    if (!date) return '';
    if (typeof date === 'string') {
      return date.split('T')[0];
    } else if (date instanceof Date) {
      return date.toISOString().split('T')[0];
    } else {
      return '';
    }
  }

  get documentType() { return this.userForm.get('documentType'); }
  get documentNumber() { return this.userForm.get('documentNumber'); }
  get name() { return this.userForm.get('name'); }
  get lastName() { return this.userForm.get('lastName'); }
  get email() { return this.userForm.get('email'); }
  get birthDate() { return this.userForm.get('birthDate'); }
  get phone() { return this.userForm.get('phone'); }

  onSubmit() {
    if (this.userForm.valid) {
      const updatedUser: UserProfile = {
        ...this.user,
        ...this.userForm.value,
        id: this.user.id
      };
      this.userService.updateUser(updatedUser).subscribe({
        next: () => {
          alert('Perfil actualizado correctamente');
          this.userForm.markAsPristine();
        },
        error: (err) => {
          console.error('Error al actualizar perfil', err);
        }
      });
    } else {
      // Marcar todos los campos como tocados para mostrar errores
      this.userForm.markAllAsTouched();
    }
  }
}
