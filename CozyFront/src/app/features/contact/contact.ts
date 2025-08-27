import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ContactService, ContactDto } from '../../services/contact/contact.service';

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

  submitting = false;
  submitSuccess: boolean | null = null;
  submitError: string | null = null;

  constructor(private fb: FormBuilder, private contactService: ContactService) {
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
    this.submitSuccess = null;
    this.submitError = null;
    if (this.contactForm.valid) {
      this.submitting = true;
      const dto: ContactDto = {
        email: this.contactForm.value.email,
        contactReason: this.contactForm.value.reason,
        message: this.contactForm.value.message
      };
      this.contactService.postContact(dto).subscribe({
        next: () => {
          this.submitSuccess = true;
          this.contactForm.reset();
        },
        error: (err) => {
          this.submitError = 'Error al enviar el formulario. Inténtalo de nuevo.';
        },
        complete: () => {
          this.submitting = false;
        }
      });
    }
  }
}
