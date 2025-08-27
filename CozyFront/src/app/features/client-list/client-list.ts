import { Component } from '@angular/core';
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

  constructor(private UserService: UserService) {}

  ngOnInit(): void {
    this.cargando = true;
    this.loadClients();
  }

  loadClients() {
    this.UserService.getUsers(this.page, this.pageSize).subscribe((result: any) => {
      if (result.items && result.totalCount !== undefined) {
        this.clients = result.items;
        this.totalCount = result.totalCount;
      } else {
        this.clients = result;
        this.totalCount = result.length;
      }
      this.cargando = false;
    }, (err: any) => {
      console.log('Error al obtener datos:', err);
      this.cargando = false;
    });
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
}
