

import { Component } from '@angular/core';
import { UserProfile } from '../../models/user';
import { Client } from '../../models/client';
import { UserService } from '../../services/user/user.service';

@Component({
  selector: 'app-client-list',
  standalone: false,
  templateUrl: './client-list.html',
  styleUrl: './client-list.css'
})

export class ClientList {
  clients: Client[] = [];
  cargando: boolean = false;
  page = 1;
  pageSize = 10;
  totalCount = 0;
  // Filtros locales
  filterDocumentNumber = '';
  filterName = '';
  filterLastName = '';
  filterEmail = '';
  filterPhone = '';
  filterRole = '';

  constructor(private UserService: UserService) {}

  ngOnInit(): void {
    this.cargando = true;
    this.loadClients();
  }

  loadClients() {
    // Construir objeto de filtros
    const filters: any = {
      documentNumber: this.filterDocumentNumber || undefined,
      name: this.filterName || undefined,
      lastName: this.filterLastName || undefined,
      email: this.filterEmail || undefined,
      phone: this.filterPhone || undefined,
      role: this.filterRole || undefined
    };
    this.UserService.getUsers(this.page, this.pageSize, filters).subscribe((result: any) => {
      let items = result.items && result.totalCount !== undefined ? result.items : result;
      this.clients = items;
      this.totalCount = result.totalCount !== undefined ? result.totalCount : items.length;
      this.cargando = false;
    }, (err: any) => {
      console.log('Error al obtener datos:', err);
      this.cargando = false;
    });
  }
  onDocumentNumberSearch(term: string) {
    this.filterDocumentNumber = term;
    this.loadClients();
  }
  onNameSearch(term: string) {
    this.filterName = term;
    this.loadClients();
  }
  onLastNameSearch(term: string) {
    this.filterLastName = term;
    this.loadClients();
  }
  onEmailSearch(term: string) {
    this.filterEmail = term;
    this.loadClients();
  }
  onPhoneSearch(term: string) {
    this.filterPhone = term;
    this.loadClients();
  }
  onRoleSearch(term: string) {
    this.filterRole = term;
    this.loadClients();
  }

  onPageChange(newPage: number) {
    this.page = newPage;
    this.loadClients();
  }

  onPageSizeChange(newSize: number) {
    this.pageSize = +newSize;
    this.page = 1;
    this.loadClients();
  }
  cambiarRol(id: number, role: string) {
    this.UserService.updateUserRole(id, role).subscribe({
      next: () => {
        alert('Rol actualizado correctamente');
        this.loadClients();
      },
      error: () => {
        alert('Error al actualizar el rol');
      }
    });
  }
}
