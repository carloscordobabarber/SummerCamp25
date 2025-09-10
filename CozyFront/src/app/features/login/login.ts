import { UserService } from '../../services/user/user.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login implements OnInit {

  loginForm!: FormGroup;
  errorMessage: string | null = null;

  constructor(private fb: FormBuilder, private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  login() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    const { email, password } = this.loginForm.value;

    this.userService.login(email, password).subscribe({
      next: res => {
        console.log('Login response:', res);
        // Guardar token en localStorage
        localStorage.setItem('token', res.token);
        // Decodificar el token para obtener id y role
        try {
          const payload = JSON.parse(atob(res.token.split('.')[1]));
          // localStorage.setItem('userId', payload.sub);
          // localStorage.setItem('userRole', payload.role);
        } catch (e) {
          console.error('Error decodificando el token', e);
        }
        alert('Login exitoso');
        this.errorMessage = '';
        this.router.navigate(['/']);
      },
      error: err => {
        console.error('Login error:', err);
        if (err.status === 404) this.errorMessage = 'Error al conectarse al servidor';
        else if (err.status === 401) this.errorMessage = 'Email o contraseña incorrectos';
        else this.errorMessage = 'Error desconocido';
      }
    });
  }

}
