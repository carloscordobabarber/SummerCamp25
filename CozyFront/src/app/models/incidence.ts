export interface Incidence {
  id: number;
  spokesperson: string;
  description: string;
  issueType: number;
  assignedCompany: string;
  createdAt: Date | string | null;
  updatedAt: Date | string | null;
  apartmentId: number;
  rentalId: number;
  tenantId: number;
  statusId: string;
}
