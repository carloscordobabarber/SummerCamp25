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
  userForm: FormGroup;
  userId = 1; // Por ejemplo, usuario actual con id=1

  constructor(private fb: FormBuilder, private userService: UserService) {
    // Inicializa el formulario con campos y validaciones
    this.userForm = this.fb.group({
      fullName: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.pattern(/^\+?\d{7,15}$/)]],  // Opcional, solo números con + opcional
    });
  }

  ngOnInit(): void {
    // Carga datos al iniciar el componente
    this.userService.getUser(this.userId).subscribe(user => {
      this.userForm.patchValue({
        documentType: user.documentType,
        documentNumber: user.documentNumber,
        name: user.name,
        lastName: user.lastName,
        email: user.email,
        phone: user.phone
      });
    });
  }

  onSubmit(): void {
    if (this.userForm.invalid) {
      this.userForm.markAllAsTouched(); // Para mostrar errores si no toca el usuario
      return;
    }

    const updatedUser: UserProfile = {
      id: this.userId,
      ...this.userForm.value
    };

    this.userService.updateUser(updatedUser).subscribe(() => {
      alert('Perfil actualizado correctamente');
    }, error => {
      alert('Error al actualizar el perfil');
    });
  }

  // Métodos de ayuda para el template
  get fullName() {
    return this.userForm.get('fullName');
  }
  get email() {
    return this.userForm.get('email');
  }
  get phoneNumber() {
    return this.userForm.get('phoneNumber');
  }
}
