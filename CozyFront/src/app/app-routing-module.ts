import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Cards } from './features/cards/cards';
import { Incidences } from './features/incidences/incidences';
import { Clients } from './features/clients/clients';
import { Contact } from './features/contact/contact';

const routes: Routes = [
  { path: '', component: Cards },
  { path: 'incidencias', component: Incidences },
  { path: 'datos-personales', component: Clients },
  { path: 'contacto', component: Contact },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
