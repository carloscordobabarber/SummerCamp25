import { Component, OnInit } from '@angular/core';
import { PaymentsService } from '../../services/payments/payments.service';
import { Payment } from '../../models/payment';

@Component({
  selector: 'app-payment-list',
  standalone: false,
  templateUrl: './payment-list.html',
  styleUrl: './payment-list.css'
})
export class PaymentList implements OnInit {
  payments: Payment[] = [];
  cargando: boolean = false;

  // Filtros
  filterStatusId: string = '';
  filterAmount: string = '';
  filterRentalId: string = '';
  filterBankAccount: string = '';
  filterStartDate: string = '';
  filterEndDate: string = '';

  // Paginación
  page = 1;
  pageSize = 10;
  totalCount = 0;

  constructor(private paymentsService: PaymentsService) {}

  ngOnInit(): void {
    this.loadPayments();
  }

  loadPayments() {
    this.cargando = true;
    this.paymentsService.getPaymentsWithFilters({
      statusId: this.filterStatusId,
      rentalId: this.filterRentalId,
      bankAccount: this.filterBankAccount,
      startDate: this.filterStartDate,
      endDate: this.filterEndDate,
      page: this.page,
      pageSize: this.pageSize
    }).subscribe((result: { items: Payment[]; totalCount: number }) => {
      let filtered = result.items;
      if (this.filterAmount) {
        filtered = filtered.filter(p => p.amount.toString().includes(this.filterAmount));
      }
      this.payments = filtered;
      this.totalCount = result.totalCount;
      this.cargando = false;
    }, () => {
      this.cargando = false;
    });
  }

  onStatusIdSearch(term: string) {
    this.filterStatusId = term;
    this.page = 1;
    this.loadPayments();
  }
  onAmountSearch(term: string) {
    this.filterAmount = term;
    this.page = 1;
    this.loadPayments();
  }
  onRentalIdSearch(term: string) {
    this.filterRentalId = term;
    this.page = 1;
    this.loadPayments();
  }
  onBankAccountSearch(term: string) {
    this.filterBankAccount = term;
    this.page = 1;
    this.loadPayments();
  }
  onDateRangeChange(range: { startDate: string, endDate: string }) {
    this.filterStartDate = range.startDate;
    this.filterEndDate = range.endDate;
    this.page = 1;
    this.loadPayments();
  }
  onPageChange(newPage: number) {
    this.page = newPage;
    this.loadPayments();
  }
  onPageSizeChange(newSize: number) {
    this.pageSize = +newSize;
    this.page = 1;
    this.loadPayments();
  }
}
