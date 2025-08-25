import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Cards } from './features/cards/cards';
import { Incidences } from './features/incidences/incidences';
import { Clients } from './features/clients/clients';
import { Contact } from './features/contact/contact';
import { About } from './features/about/about';
import { CardManager } from './features/card-manager/card-manager';
import { Profile } from './features/profile/profile';

const routes: Routes = [
  { path: '', component: CardManager },
  { path: 'incidences', component: Incidences },
  { path: 'register', component: Clients },
  { path: 'profile', component: Profile },
  { path: 'contact', component: Contact },
  { path: 'about', component: About },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
