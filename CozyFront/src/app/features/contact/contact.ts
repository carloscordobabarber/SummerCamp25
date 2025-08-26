import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-contact',
  standalone: false,
  templateUrl: './contact.html',
  styleUrl: './contact.css'
})
export class Contact {
  contactForm: FormGroup;
  reasons = [
    { value: 'trabaja', label: 'Trabaja con nosotros' },
    { value: 'reclamaciones', label: 'Reclamaciones' },
    { value: 'solicitudes', label: 'Solicitudes' },
    { value: 'otros', label: 'Otros' }
  ];

  constructor(private fb: FormBuilder) {
    this.contactForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      reason: ['', Validators.required],
      message: ['', [Validators.required, Validators.maxLength(1000)]]
    });
  }

  get email() { return this.contactForm.get('email'); }
  get reason() { return this.contactForm.get('reason'); }
  get message() { return this.contactForm.get('message'); }

  onSubmit() {
    if (this.contactForm.valid) {
      alert('Formulario enviado correctamente');
      this.contactForm.reset();
    }
  }
}
