import { Component, Input, Output, EventEmitter, SimpleChanges, OnChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PaymentsService } from '../../../services/payments/payments.service';
import { RentalsService } from '../../../services/rentals/rentals.service';
import { Payment } from '../../../models/payment';
import { ElPayment } from '../../../models/el-payment';
import { ElPaymentService } from '../../../services/el-Payment/el-payment.service';

@Component({
  selector: 'app-payment-form',
  templateUrl: './payment-form.html',
  styleUrls: ['./payment-form.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class PaymentFormComponent implements OnChanges {
  @Input() rental: any;
  @Input() user: any;
  @Output() paymentSuccess = new EventEmitter<void>();
  @Output() paymentCancel = new EventEmitter<void>();

  paymentForm!: FormGroup;
  loading = false;
  error: string | null = null;

  constructor(
    private fb: FormBuilder,
    private paymentsService: PaymentsService,
    private rentalsService: RentalsService,
    private elPaymentService: ElPaymentService
  ) { }

  ngOnInit() {
    this.initForm();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['rental'] && changes['rental'].currentValue) {
      this.initForm();
    }
  }

  private initForm() {
    this.paymentForm = this.fb.group({
      amount: [this.rental?.apartmentPrice, [Validators.required, Validators.min(1)]],
      rentalId: [this.rental ? this.rental.rentalId : '', Validators.required],
      bankAccount: ['', Validators.required],
      paymentDate: [new Date().toISOString().substring(0, 10), Validators.required]
    });
  }

  submit() {
    if (this.paymentForm.invalid) {
      console.log('Formulario inválido:', this.paymentForm.value, this.paymentForm.status);
      return;
    }
    this.loading = true;
    const payment: Payment = {
      ...this.paymentForm.value,
      statusId: 'G',
      amount: this.paymentForm.value.amount,
      rentalId: this.paymentForm.value.rentalId,
      bankAccount: this.paymentForm.value.bankAccount,
      paymentDate: this.paymentForm.value.paymentDate
    };
    console.log('Enviando pago:', payment);
    this.paymentsService.createPayment(payment).subscribe({
      next: () => {
        // Actualizar el statusId del alquiler a 'A'
        this.rentalsService.updateRental(payment.rentalId, {
          id: payment.rentalId,
          userId: this.rental.userId,
          apartmentId: this.rental.apartmentId,
          startDate: this.rental.startDate,
          endDate: this.rental.endDate,
          statusId: 'A'
        }).subscribe({
          next: () => {
            this.loading = false;
            this.paymentSuccess.emit();
          },
          error: err => {
            this.loading = false;
            this.error = 'Pago realizado, pero no se pudo actualizar el estado del alquiler';
            console.error('Error al actualizar alquiler:', err);
          }
        });
      },
      error: err => {
        this.loading = false;
        this.error = 'Error al realizar el pago';
        console.error('Error en el pago:', err);
      }
    });
    // Construir el objeto ElPayment
    const apartmentCode = this.rental?.apartmentCode || this.rental?.code || '';
    const amount = this.paymentForm.value.amount;
    const description = `Pago del apartamento ${apartmentCode}: ${amount}`;
    const elPayment: ElPayment = {
      description,
      apartmentCode,
      amount
    };
    this.elPaymentService.postPayment(elPayment).subscribe({
      next: () => {
        this.loading = false;
        this.paymentSuccess.emit();
      },
      error: (err: any) => {
        this.loading = false;
        this.error = 'Error al enviar el pago a ElPayment';
        console.error('Error en ElPayment:', err);
      }
    });
  }

  cancel() {
    this.paymentCancel.emit();
  }
}
