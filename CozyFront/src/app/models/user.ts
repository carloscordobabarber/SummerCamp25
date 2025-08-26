export interface UserLogin {
  email: string;
  password: string;
}
    
export interface UserProfile {
  id: number;
  documentType: string;
  documentNumber: string;
  name: string;
  lastName: string;
  email: string;
  birthDate: Date;
  phone?: string;
  role: string;
}
