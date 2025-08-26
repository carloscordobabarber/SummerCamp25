import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login implements OnInit {

  loginForm!: FormGroup;
  errorMessage: string | null = null;

  // constructor(private fb: FormBuilder, private authService: AuthService) {
  constructor(private fb: FormBuilder, private http: HttpClient) { }

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

     this.http.post<any>('https://tuapi.com/api/auth/login', { email, password }).subscribe({
      next: res => {
        alert('Login exitoso');
        this.errorMessage = '';
        // Guardar token o redirigir
      },
      error: err => {
        this.errorMessage = err.error || 'Email o contraseña incorrectos';
      }
    });
  }

}
