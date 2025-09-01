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

  // Admin list
  contacts: any[] = [];
  filterEmail: string = '';
  filterContactReason: string = '';
  page = 1;
  pageSize = 10;
  totalCount = 0;
  cargando = false;

  contactReasonOptions = [
    'Trabaja con nosotros',
    'Reclamaciones',
    'Solicitudes',
    'Otros'
  ];

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

  get isAdmin(): boolean {
    try {
      const user = localStorage.getItem('userRole');
      if (!user) return false;
      return user === 'Admin';
    } catch {
      return false;
    }
  }

  // Métodos para la lista de contactos admin
  ngOnInit() {
    if (this.isAdmin) {
      this.loadContacts();
    }
  }

  loadContacts() {
    this.cargando = true;
    this.contactService.getContactsWithFilters({
      email: this.filterEmail,
      contactReason: this.filterContactReason,
      page: this.page,
      pageSize: this.pageSize
    }).subscribe((result: { items: any[]; totalCount: number }) => {
      this.contacts = result.items;
      this.totalCount = result.totalCount;
      this.cargando = false;
    }, () => {
      this.cargando = false;
    });
  }

  onEmailSearch(term: string) {
    this.filterEmail = term;
    this.page = 1;
    this.loadContacts();
  }
  onContactReasonChange(val: string) {
    // Map label to value for API
    switch (val) {
      case 'Trabaja con nosotros':
        this.filterContactReason = 'trabaja'; break;
      case 'Reclamaciones':
        this.filterContactReason = 'reclamaciones'; break;
      case 'Solicitudes':
        this.filterContactReason = 'solicitudes'; break;
      case 'Otros':
        this.filterContactReason = 'otros'; break;
      default:
        this.filterContactReason = '';
    }
    this.page = 1;
    this.loadContacts();
  }
  onPageChange(newPage: number) {
    this.page = newPage;
    this.loadContacts();
  }
  onPageSizeChange(newSize: number) {
    this.pageSize = +newSize;
    this.page = 1;
    this.loadContacts();
  }
}
