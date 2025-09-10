import { Component, OnInit } from '@angular/core';
import { ChangePassService } from '../../../services/change-pass/change-pass.service';
import { UserService } from '../../../services/user/user.service';
import { ChangePass } from '../../../models/change-pass';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { AbstractControl, FormBuilder, FormGroup, Validators, ValidationErrors } from '@angular/forms';

@Component({
  selector: 'app-change-password',
  standalone: false,
  templateUrl: './change-password.html',
  styleUrl: './change-password.css'
})
export class ChangePassword implements OnInit {
  // Validación de contraseña segura (igual que clients)
  private passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]).{8,}$/;


  showCurrentPassword = false;
  showNewPassword = false;
  showConfirmPassword = false;
  passwordForm!: FormGroup;

  constructor(private fb: FormBuilder, private changePassService: ChangePassService, private userService: UserService) {}

  ngOnInit(): void {
    this.passwordForm = this.fb.group({
      currentPassword: ['', [Validators.required, Validators.minLength(6)]],
      newPassword: ['', [Validators.required, Validators.maxLength(24), Validators.pattern(this.passwordPattern)]],
      confirmPassword: ['', Validators.required]
    }, {
      validators: this.passwordMatchValidator
    });
  }

  // Validador personalizado para coincidencia de contraseñas
  passwordMatchValidator(form: AbstractControl): ValidationErrors | null {
    const password = form.get('newPassword')?.value;
    const confirmPassword = form.get('confirmPassword')?.value;
    if (password !== confirmPassword) {
      return { passwordMismatch: true };
    }
    return null;
  }

  get currentPassword() {
    return this.passwordForm.get('currentPassword');
  }

  get newPassword() {
    return this.passwordForm.get('newPassword');
  }

  get confirmPassword() {
    return this.passwordForm.get('confirmPassword');
  }

  onSubmit(): void {
    if (this.passwordForm.valid) {
      const token = this.userService.getToken();
      if (!token) {
        alert('No se ha encontrado usuario logueado. Por favor, inicia sesión.');
        return;
      }
      const dto: ChangePass = {
        oldPassword: this.passwordForm.value.currentPassword,
        newPassword: this.passwordForm.value.newPassword
      };
      this.changePassService.changePassword(dto, token).subscribe({
        next: (res) => {
          alert('Contraseña cambiada con éxito!');
          this.passwordForm.reset();
        },
        error: (err) => {
          alert(err?.error || 'Error al cambiar la contraseña');
        }
      });
    }
  }
}
