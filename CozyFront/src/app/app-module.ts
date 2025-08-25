import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { Header } from './shared/header/header';
import { Footer } from './shared/footer/footer';
import { Cards } from './features/cards/cards';
import { ApartmentList } from './features/apartment-list/apartment-list';
import { Incidences } from './features/incidences/incidences';
import { Clients } from './features/clients/clients';
import { Contact } from './features/contact/contact';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { IncidenceForm } from './features/incidences/components/incidence-form/incidence-form';
import { IncidenceViewer } from './features/incidences/components/incidence-viewer/incidence-viewer';
import { About } from './features/about/about';
import { CardManager } from './features/card-manager/card-manager';
import { Paginador } from './features/paginador/paginador';
import { Login } from './features/login/login';
import { Profile } from './features/profile/profile';
import { ClientList } from './features/client-list/client-list';
import { IncidenceList } from './features/incidence-list/incidence-list';
import { SearchBar } from './shared/search-bar/search-bar';
import { SliderFilter } from './shared/slider-filter/slider-filter';

@NgModule({
  declarations: [
    App,
    Header,
    Footer,
    Cards,
    ApartmentList,
    Clients,
    Contact,
    About,
    CardManager,
    Login,
    Profile,
    ClientList,
    IncidenceList,
    SearchBar,
    SliderFilter
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    Incidences,
    IncidenceForm,
    IncidenceViewer,
    Paginador
  ],
  providers: [
    provideBrowserGlobalErrorListeners()
  ],
  bootstrap: [App]
})
export class AppModule { }
