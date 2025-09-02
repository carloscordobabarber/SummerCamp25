import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PaymentsService } from '../../../services/payments/payments.service';
import { Payment } from '../../../models/payment';

@Component({
  selector: 'app-payment-form',
  templateUrl: './payment-form.html',
  styleUrls: ['./payment-form.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class PaymentFormComponent {
  @Input() rental: any;
  @Input() user: any;
  @Output() paymentSuccess = new EventEmitter<void>();
  @Output() paymentCancel = new EventEmitter<void>();

  paymentForm!: FormGroup;
  loading = false;
  error: string | null = null;

  constructor(
    private fb: FormBuilder,
    private paymentsService: PaymentsService
  ) {}

  ngOnInit() {
    this.paymentForm = this.fb.group({
      amount: [this.rental?.apartmentPrice, [Validators.required, Validators.min(1)]],
      rentalId: [this.rental?.id, Validators.required],
      bankAccount: ['', Validators.required],
      paymentDate: [new Date().toISOString().substring(0, 10), Validators.required]
    });
  }

  submit() {
    if (this.paymentForm.invalid) return;
    this.loading = true;
    const payment: Payment = this.paymentForm.value;
    payment.statusId = '1'; // Asume '1' es pagado, ajusta según tu lógica
    this.paymentsService.createPayment(payment).subscribe({
      next: () => {
        this.loading = false;
        this.paymentSuccess.emit();
      },
      error: err => {
        this.loading = false;
        this.error = 'Error al realizar el pago';
      }
    });
  }

  cancel() {
    this.paymentCancel.emit();
  }
}
