import { AfterViewInit, Component, ElementRef, Input, ViewChild, OnInit } from '@angular/core';
import { UserService } from '../../../services/user/user.service';
import { UserRental } from '../../../models/user-rental';

declare var bootstrap: any;

// Puedes adaptar la interfaz si necesitas más campos

@Component({
  selector: 'app-my-contracts',
  standalone: false,
  templateUrl: './my-contracts.html',
  styleUrl: './my-contracts.css'
})
export class MyContracts implements AfterViewInit, OnInit {
  @Input() user: any;
  contratos: UserRental[] = [];
  contratoSeleccionado: UserRental | null = null;
  @ViewChild('contratoModal') contratoModal!: ElementRef;
  private modalInstance: any;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    const userIdStr = localStorage.getItem('userId');
    const userId = userIdStr ? parseInt(userIdStr, 10) : null;
    if (userId) {
      this.userService.getUserRentals(userId).subscribe({
        next: (data) => this.contratos = data,
        error: (err) => console.error('Error al cargar contratos', err)
      });
    }
  }

  ngAfterViewInit() {
    this.modalInstance = new bootstrap.Modal(this.contratoModal.nativeElement);
    // Limpia el contrato seleccionado al cerrar el modal
    this.contratoModal.nativeElement.addEventListener('hidden.bs.modal', () => {
      this.contratoSeleccionado = null;
    });
  }

  abrirContrato(contrato: UserRental) {
    this.contratoSeleccionado = contrato;
    this.modalInstance.show();

    setTimeout(() => {
      const modalElement = this.contratoModal.nativeElement as HTMLElement;
      const focusable = modalElement.querySelector('button, [href], input, select, textarea, [tabindex]:not([tabindex="-1"])') as HTMLElement;
      if (focusable) {
        focusable.focus();
      } else {
        modalElement.focus();
      }
    }, 100);
  }

  cerrarContrato() {
    this.modalInstance.hide();
  }
}
