import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IncidencesService } from '../../../../services/incidences/incidences.service';
import { CfIncidenceService } from '../../../../services/cf-incidence/cf-incidence.service';
import { UserRentalsService } from '../../../../services/user/user-rentals.service';
import { UserService } from '../../../../services/user/user.service';
import { UserRental } from '../../../../models/user-rental';
import { Incidence } from '../../../../models/incidence';
import { CF_Incidence } from '../../../../models/cf-incidence';

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
  selectedRentalId: number | null = null;

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
    private userRentalsService: UserRentalsService,
    private userService: UserService,
    private cfIncidenceService: CfIncidenceService,
  ) {
    this.incidenceForm = this.fb.group({
      spokesperson: [' ', [Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(1000)]],
      issueType: [6, [Validators.required]],
      apartmentId: ['', [Validators.required]],
      rentalId: [{ value: null, disabled: true }]
    });

    const userId = this.userService.getUserIdFromToken();
    this.userRole = this.userService.getRoleFromToken() || '';

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
        apartmentId: rental.apartmentId
      });
      this.selectedRentalId = rental.rentalId;
    } else {
      console.warn('[onRentalChange] No se encontró rental para apartmentId:', apartmentId);
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
      const user_rental = this.userRentals.find(r => r.apartmentId === Number(formValue.apartmentId) && r.rentalId === this.selectedRentalId);
      if (!user_rental) {
        alert('El número de contrato no corresponde al apartamento seleccionado.');
        return;
      }
      console.log('user_rental:', user_rental);
      const tenantId = this.userService.getUserIdFromToken();
      if (tenantId === null) {
        alert('No se ha encontrado usuario logueado. Por favor, inicia sesión.');
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
        rentalId: Number(this.selectedRentalId),
        tenantId,
        statusId: 'P'
      };

      this.incidencesService.createIncidence(incidence).subscribe({
        next: (res) => {
          // Crear incidencia en CozyFront API si la anterior fue exitosa
          const newIncidenceId = res && res.id ? res.id : 0;
          const cf_address = `${user_rental.streetName} ${user_rental.portal || ''}, ${user_rental.floor || ''} ${user_rental.apartmentDoor || ''}`.trim();
          const cfIncidence: CF_Incidence = {
            incidenceId: newIncidenceId,
            issueTypeId: formValue.issueType,
            description: formValue.description,
            address: cf_address,
            surface: user_rental.apartmentArea
          };
          this.cfIncidenceService.createIncidence(cfIncidence).subscribe({
            next: () => {
              alert('Incidencia enviada correctamente a CozyFront y API externa');
              this.selectedRentalId = null;
              this.incidenceForm.reset();
            },
            error: () => {
              alert('Incidencia guardada en CozyFront pero error al enviar a API externa');
              this.incidenceForm.reset();
            }
          });
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

