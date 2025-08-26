import { Component, OnInit } from '@angular/core';
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
  // userForm: FormGroup;
  // userId = 1; // Por ejemplo, usuario actual con id=1

  // constructor(private fb: FormBuilder, private userService: UserService) {
  //   // Inicializa el formulario con campos y validaciones
  //   this.userForm = this.fb.group({
  //     fullName: ['', [Validators.required, Validators.minLength(3)]],
  //     email: ['', [Validators.required, Validators.email]],
  //     phoneNumber: ['', [Validators.pattern(/^\+?\d{7,15}$/)]],  // Opcional, solo números con + opcional
  //   });
  // }

  // ngOnInit(): void {
  //   // Carga datos al iniciar el componente
  //   this.userService.getUser(this.userId).subscribe(user => {
  //     this.userForm.patchValue({
  //       documentType: user.documentType,
  //       documentNumber: user.documentNumber,
  //       name: user.name,
  //       lastName: user.lastName,
  //       email: user.email,
  //       phone: user.phone
  //     });
  //   });
  // }

  // onSubmit(): void {
  //   if (this.userForm.invalid) {
  //     this.userForm.markAllAsTouched(); // Para mostrar errores si no toca el usuario
  //     return;
  //   }

  //   const updatedUser: UserProfile = {
  //     id: this.userId,
  //     ...this.userForm.value
  //   };

  //   this.userService.updateUser(updatedUser).subscribe(() => {
  //     alert('Perfil actualizado correctamente');
  //   }, error => {
  //     alert('Error al actualizar el perfil');
  //   });
  // }

  // // Métodos de ayuda para el template
  // get fullName() {
  //   return this.userForm.get('fullName');
  // }
  // get email() {
  //   return this.userForm.get('email');
  // }
  // get phoneNumber() {
  //   return this.userForm.get('phoneNumber');
  // }

   userForm!: FormGroup;
   userName: string;

  constructor(private fb: FormBuilder) {
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
      phone: ['', [Validators.pattern(/^\+\d{7,15}$/)]], // opcional, pero si se pone debe ser formato internacional
    });
  }

  get documentType() {
    return this.userForm.get('documentType');
  }
  get documentNumber() {
    return this.userForm.get('documentNumber');
  }
  get name() {
    this.userName = this.userForm.get('name')?.value;
    return this.userForm.get('name');
  }
  get lastName() {
    return this.userForm.get('lastName');
  }
  get email() {
    return this.userForm.get('email');
  }
  get birthDate() {
    return this.userForm.get('birthDate');
  }
  get phone() {
    return this.userForm.get('phone');
  }

  onSubmit() {
    if (this.userForm.valid) {
      console.log('Formulario válido', this.userForm.value);
      // Aquí puedes hacer el update en backend o lógica que necesites
    } else {
      // Marcar todos los campos como tocados para mostrar errores
      this.userForm.markAllAsTouched();
    }
  }
}
