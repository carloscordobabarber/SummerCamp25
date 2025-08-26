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

    // this.http.post<any>('https://devdemoapi4.azurewebsites.net/api/Login/login', { Email: email, Password: password }).subscribe({
    //   next: res => {
    //     alert('Login exitoso');
    //     this.errorMessage = '';
    //     // Guardar token o redirigir
    //   },
    //   error: err => {
    //     if (err.status === 404) this.errorMessage = 'Error al conectarse al servidor';
    //     if (err.status === 401) this.errorMessage = 'Email o contraseña incorrectos';
    //   }
    // });

    this.http.post('https://devdemoapi4.azurewebsites.net/api/Login/login', { Email: email, Password: password }, { responseType: 'text' }).subscribe({
      next: res => {
        alert(res); // aquí res es string: "Login exitoso."
        this.errorMessage = '';
      },
      error: err => {
        if (err.status === 404) this.errorMessage = 'Error al conectarse al servidor';
        if (err.status === 401) this.errorMessage = 'Email o contraseña incorrectos';
      }
    });
  }

}
