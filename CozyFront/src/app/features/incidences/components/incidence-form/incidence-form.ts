
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

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

  constructor(private fb: FormBuilder) {
    this.incidenceForm = this.fb.group({
      spokesperson: [
        ' ',
        [Validators.maxLength(100)]
      ],
      description: [
        '',
        [Validators.required, Validators.maxLength(1000)]
      ],
  issueType: [6, [Validators.required]]
    });
  }

  get spokesperson() {
    return this.incidenceForm.get('spokesperson');
  }

  get description() {
    return this.incidenceForm.get('description');
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
      console.log(formValue);
      // Aquí puedes enviar formValue a la API
    } else {
      this.incidenceForm.markAllAsTouched();
    }
  }
}
