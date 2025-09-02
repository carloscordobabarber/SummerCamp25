
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IncidencesService } from '../../../../services/incidences/incidences.service';
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

  incidenceTypes = [
    'Avería eléctrica',
    'Fuga de agua',
    'Problema de calefacción',
    'Daños estructurales',
    'Plagas',
    'Problemas de acceso',
    'Otros'
  ];

  constructor(private fb: FormBuilder, private incidencesService: IncidencesService) {
    this.incidenceForm = this.fb.group({
      spokesperson: [
        ' ',
        [Validators.maxLength(100)]
      ],
      description: [
        '',
        [Validators.required, Validators.maxLength(1000)]
      ],
      issueType: [6, [Validators.required]],
      apartmentId: [null, [Validators.required, Validators.min(1)]],
      rentalId: [null, [Validators.required, Validators.min(1)]]
    });
  }

  get spokesperson() {
    return this.incidenceForm.get('spokesperson');
  }
  get description() {
    return this.incidenceForm.get('description');
  }
  get apartmentId() {
    return this.incidenceForm.get('apartmentId');
  }
  get rentalId() {
    return this.incidenceForm.get('rentalId');
  }

  onSubmit() {
    if (this.incidenceForm.valid) {
      const formValue = { ...this.incidenceForm.value };
      // Asegurarse de que spokesperson nunca sea null ni vacío
      if (!formValue.spokesperson || formValue.spokesperson.trim() === '') {
        formValue.spokesperson = ' ';
      }
      // issueType ya es un número del 0 al 6
      formValue.issueType = Number(formValue.issueType);

      // Construir el objeto Incidence para la API
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
