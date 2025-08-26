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

const routes: Routes = [
  { path: '', component: CardManager },
  { path: 'incidences', component: Incidences },
  { path: 'register', component: Clients },
  { path: 'profile', component: Profile },
  { path: 'apartment-details/:id', component: ApartmentDetails },
  { path: 'apartment-list', component: ApartmentList },
  { path: 'client-list', component: ClientList },
  { path: 'incidence-list', component: IncidenceList },
  { path: 'contact', component: Contact },
  { path: 'about', component: About },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
