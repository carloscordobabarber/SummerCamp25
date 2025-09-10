import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Cards } from './features/cards/cards';
import { Incidences } from './features/incidences/incidences';
import { Clients } from './features/clients/clients';
import { Contact } from './features/contact/contact';
import { About } from './features/about/about';
import { CardManager } from './features/card-manager/card-manager';
import { Profile } from './features/profile/profile';
import { ApartmentList } from './features/apartment-list/apartment-list';
import { ClientList } from './features/client-list/client-list';
import { IncidenceList } from './features/incidence-list/incidence-list';
import { ApartmentDetails } from './features/apartment-details/apartment-details';
import { Login } from './features/login/login';
import { ProfileDetails } from './features/profile/profile-details/profile-details';
import { MyRentals } from './features/profile/my-rentals/my-rentals';
import { MyContracts } from './features/profile/my-contracts/my-contracts';
import { ContractList } from './features/contract-list/contract-list';
import { PaymentList } from './features/payment-list/payment-list';
import { RoleGuard } from './guards/role.guard';

const routes: Routes = [
  { path: '', component: CardManager },
  { path: 'register', component: Clients },
  { path: 'apartment-details/:id', component: ApartmentDetails },
  { path: 'login', component: Login },
  { path: 'profile', component: Profile, canActivate: [RoleGuard], data: { roles: ['Client', 'Admin'] } },
  { path: 'apartment-list', component: ApartmentList, canActivate: [RoleGuard], data: { roles: ['Admin'] } },
  { path: 'client-list', component: ClientList, canActivate: [RoleGuard], data: { roles: ['Admin'] } },
  { path: 'incidence-list', component: IncidenceList, canActivate: [RoleGuard], data: { roles: ['Admin'] } },
  { path: 'contract-list', component: ContractList, canActivate: [RoleGuard], data: { roles: ['Admin'] } },
  { path: 'payment-list', component: PaymentList, canActivate: [RoleGuard], data: { roles: ['Admin'] } },
  { path: 'contact', component: Contact },
  { path: 'about', component: About },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
