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
import { RangeSliderFilter } from './shared/slider-filter/range-slider-filter';
import { SelectFilterComponent } from './shared/select-filter/select-filter';
import { ChatBot } from './shared/chat-bot/chat-bot';
import { ApartmentDetails } from './features/apartment-details/apartment-details';
import { ProfileDetails } from './features/profile/profile-details/profile-details';
import { MyRentals } from './features/profile/my-rentals/my-rentals';
import { MyContracts } from './features/profile/my-contracts/my-contracts';

import { DateRangeFilter } from './shared/date-filter/date-range-filter';
import { ContractList } from './features/contract-list/contract-list';
import { DateFilter } from './shared/date-filter/date-filter';
import { PaymentList } from './features/payment-list/payment-list';
import { PaymentFormComponent } from './features/profile/payment-form/payment-form';
import { ChangePassword } from './features/profile/change-password/change-password';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    ChangePassword,
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
    SliderFilter,
    RangeSliderFilter,
    ChatBot,
    ApartmentDetails,
    ProfileDetails,
    MyRentals,
    MyContracts,
    SelectFilterComponent,
    ContractList,
    DateRangeFilter,
    DateFilter,
    PaymentList
    
  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    Incidences,
    IncidenceForm,
    IncidenceViewer,
    Paginador,
    PaymentFormComponent
  ],
  providers: [
    provideBrowserGlobalErrorListeners()
  ],
  bootstrap: [App]
})
export class AppModule { }
