import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IncidencesService } from '../../../../services/incidences/incidences.service';
import { UserRentalsService } from '../../../../services/user/user-rentals.service';
import { UserRental } from '../../../../models/user-rental';
import { Incidence } from '../../../../models/incidence';

@Component({
  selector: 'app-incidence-form',
  standalone: true,
  templateUrl: './incidence-form.html',
  styleUrl: './incidence-form.css',
  imports: [ReactiveFormsModule, CommonModule]
})
export class IncidenceForm {
  incidenceForm: FormGroup;
  userRentals: UserRental[] = [];
  userRole: string = '';

  incidenceTypes = [
    'Avería eléctrica',
    'Fuga de agua',
    'Problema de calefacción',
    'Daños estructurales',
    'Plagas',
    'Problemas de acceso',
    'Otros'
  ];

  constructor(
    private fb: FormBuilder,
    private incidencesService: IncidencesService,
    private userRentalsService: UserRentalsService
  ) {
    this.incidenceForm = this.fb.group({
      spokesperson: [' ', [Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(1000)]],
      issueType: [6, [Validators.required]],
      apartmentId: ['', [Validators.required]],
  rentalId: [null]
    });

    const userId = localStorage.getItem('userId');
    this.userRole = localStorage.getItem('role') || '';

    if (this.userRole !== 'Admin' && userId) {
      // Solo carga los alquileres del usuario logueado
      this.userRentalsService.getRentalsByUserId(Number(userId)).subscribe({
            next: (rentals) => {
              const activos = rentals.filter(r => r.statusId === 'A');
              this.userRentals = activos;
              console.log('[Nueva incidencia] Rentals activos traídos:', activos);
        },
        error: () => this.userRentals = []
      });
    } else {
      // Si es Admin, podrías cargar todos los alquileres (requiere otro endpoint)
      this.userRentals = [];
    }
  }

  get spokesperson() { return this.incidenceForm.get('spokesperson'); }
  get description() { return this.incidenceForm.get('description'); }
  get apartmentId() { return this.incidenceForm.get('apartmentId'); }

  get rentalId() { return this.incidenceForm.get('rentalId'); }

  onRentalChange(event: Event) {
    const select = event.target as HTMLSelectElement;
    const apartmentId = Number(select.value);
    const rental = this.userRentals.find(r => r.apartmentId === apartmentId);
    if (rental) {
      this.incidenceForm.patchValue({
        apartmentId: rental.apartmentId,
        rentalId: rental.rentalId
      });
    }
  }

  onSubmit() {
    if (this.incidenceForm.valid) {
      const formValue = { ...this.incidenceForm.value };
      if (!formValue.spokesperson || formValue.spokesperson.trim() === '') {
        formValue.spokesperson = ' ';
      }
      formValue.issueType = Number(formValue.issueType);

      // Validación: rentalId debe corresponder al apartmentId seleccionado
      const rental = this.userRentals.find(r => r.apartmentId === Number(formValue.apartmentId) && r.rentalId === Number(formValue.rentalId));
      if (!rental) {
        alert('El número de contrato no corresponde al apartamento seleccionado.');
        return;
      }

      const incidence: Incidence = {
        id: 0,
        spokesperson: formValue.spokesperson,
        description: formValue.description,
        issueType: formValue.issueType,
        assignedCompany: '',
        createdAt: new Date().toISOString(),
        updatedAt: null,
        apartmentId: Number(formValue.apartmentId),
        rentalId: Number(formValue.rentalId),
        tenantId: Number(localStorage.getItem('userId')),
        statusId: 'P'
      };

      this.incidencesService.createIncidence(incidence).subscribe({
        next: (res) => {
          alert('Incidencia enviada correctamente');
          this.incidenceForm.reset();
        },
        error: (err) => {
          alert('Error al enviar la incidencia');
        }
      });
    } else {
      this.incidenceForm.markAllAsTouched();
    }
  }
}

