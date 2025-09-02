export interface Payment {
  id: number;
  statusId: string;
  amount: number;
  rentalId: number;
  paymentDate: string;
  bankAccount: string;
}