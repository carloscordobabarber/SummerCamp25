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

  constructor(private UserService: UserService) {}

  ngOnInit(): void {
    this.cargando = true;
    this.UserService.getUsers().subscribe({
      next: (data) => {
          this.clients = data; 
          console.log('Datos recibidos:', this.clients);
          this.cargando = false;
        },
        error: (err) => {
          console.log('Error al obtener datos:', err);
          this.cargando = false;
        }
      });
    }
}
