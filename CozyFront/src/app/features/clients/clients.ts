



import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { UserService } from '../../services/user/user.service';

@Component({
  selector: 'app-clients',
  standalone: false,
  templateUrl: './clients.html',
  styleUrl: './clients.css'
})
export class Clients implements OnInit {
  clientForm!: FormGroup;
  submitted = false;
  apiError: string | null = null;

  constructor(private fb: FormBuilder, private userService: UserService, private router: Router) {}

  ngOnInit(): void {
    this.clientForm = this.fb.group({
      documentType: [
        'dni',
        [Validators.required]
      ],
      documentNumber: [
        '',
        [Validators.required, Validators.maxLength(9), this.documentNumberValidator.bind(this)]
      ],
      name: [
        '',
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(36),
          Validators.pattern(/^[\p{L}\s\-'’]+$/u)
        ]
      ],
      lastName: [
        '',
        [
          Validators.required,
          Validators.maxLength(36),
          Validators.pattern(/^[\p{L}\s\-'’]+$/u)
        ]
      ],
      birthDate: ['', [Validators.required, this.birthDateValidator]],
      email: [
        '',
        [
          Validators.required,
          Validators.maxLength(100),
          Validators.pattern(/^[a-zA-Z0-9._]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/)
        ]
      ],
      confirmEmail: [''],
      phone: [
        '',
        [
          Validators.required,
          Validators.maxLength(20),
          Validators.pattern(/^\+?\d+$/)
        ]
      ],
      password: [
        '',
        [
          Validators.required,
          Validators.maxLength(24),
          Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]).{8,}$/)
        ]
      ],
      confirmPassword: [''],
      role: [
        'Client',
        [Validators.required, Validators.maxLength(6)]
      ]
    }, {
      validators: [this.emailMatchValidator, this.passwordMatchValidator]
    });

    // Actualizar validación de documentNumber al cambiar documentType
    this.clientForm.get('documentType')?.valueChanges.subscribe(() => {
      this.clientForm.get('documentNumber')?.updateValueAndValidity();
    });
  }

  public get emailMismatch() {
    return this.clientForm.errors?.['emailMismatch'] && this.submitted;
  }

  public get passwordMismatch() {
    return this.clientForm.errors?.['passwordMismatch'] && this.submitted;
  }

  get f() {
    return this.clientForm.controls;
  }

  get birthDateError() {
    const errors = this.f['birthDate'].errors;
    if (!errors) return null;
    if (errors['birthDateMin']) return errors['birthDateMin'];
    if (errors['birthDateMax']) return errors['birthDateMax'];
    if (errors['birthDateAge']) return errors['birthDateAge'];
    return null;
  }

  get documentNumberFormatError() {
    const errors = this.f['documentNumber'].errors;
    return errors && errors['documentNumberFormat'] ? errors['documentNumberFormat'] : null;
  }

  // Validador personalizado para fecha de nacimiento
  birthDateValidator = (control: AbstractControl): ValidationErrors | null => {
    if (!control.value) return null;
    const value = new Date(control.value);
    const minDate = new Date('1900-01-01');
    const today = new Date();
    value.setHours(0,0,0,0);
    today.setHours(0,0,0,0);
    if (value < minDate) {
      return { birthDateMin: 'La fecha no puede ser anterior al 01/01/1900.' };
    }
    if (value > today) {
      return { birthDateMax: 'La fecha no puede ser posterior a hoy.' };
    }
    const minAgeDate = new Date(today.getFullYear() - 18, today.getMonth(), today.getDate());
    if (value > minAgeDate) {
      return { birthDateAge: 'Debes tener al menos 18 años.' };
    }
    return null;
  }

  // Validadores de igualdad para email y contraseña
  emailMatchValidator(form: AbstractControl): ValidationErrors | null {
    const email = form.get('email')?.value;
    const confirmEmail = form.get('confirmEmail')?.value;
    if (email !== confirmEmail) {
      return { emailMismatch: true };
    }
    return null;
  }

  passwordMatchValidator(form: AbstractControl): ValidationErrors | null {
    const password = form.get('password')?.value;
    const confirmPassword = form.get('confirmPassword')?.value;
    if (password !== confirmPassword) {
      return { passwordMismatch: true };
    }
    return null;
  }

  documentNumberValidator(control: AbstractControl): ValidationErrors | null {
    if (!control || !control.parent) return null;
    const type = control.parent.get('documentType')?.value;
    const number = control.value;
    if (!type || !number) return null;
    const dniRegex = /^\d{8}[A-Z]$/;
    const nieRegex = /^[XYZ]\d{7}[A-Z]$/;
    if (type === 'dni' && !dniRegex.test(number)) {
      return { documentNumberFormat: 'El número debe ser un DNI válido (8 dígitos y una letra).' };
    }
    if (type === 'nie' && !nieRegex.test(number)) {
      return { documentNumberFormat: 'El número debe ser un NIE válido ([X/Y/Z] + 7 dígitos y una letra).' };
    }
    return null;
  }

  onSubmit() {
    this.submitted = true;
    this.apiError = null;
    if (this.clientForm.invalid) {
      return;
    }
    // Solo enviar los campos originales, no los de confirmación
    const formValue = { ...this.clientForm.value };
    formValue.statusId = 'A';
    delete formValue.confirmEmail;
    delete formValue.confirmPassword;
    this.userService.createUser(formValue).subscribe({
      next: (res) => {
        alert('¡Usuario registrado correctamente!');
        this.clientForm.reset();
        this.submitted = false;
        this.router.navigate(['/login']);
      },
      error: (err) => {
        this.apiError = err?.error?.message || 'Error al registrar el usuario.';
      }
    });
  }
}
