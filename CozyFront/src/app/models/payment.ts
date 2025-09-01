export interface Payment {
  id: number;
  statusId: number;
  amount: number;
  rentalId: number;
  paymentDate: string;
  bankAccount: string;
}