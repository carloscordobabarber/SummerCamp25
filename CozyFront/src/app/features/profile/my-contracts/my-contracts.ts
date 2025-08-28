import { AfterViewInit, Component, ElementRef, Input, ViewChild } from '@angular/core';

declare var bootstrap: any;

interface Contrato {
  id: number;
  nombre: string;
  fechaInicio: string;
  fechaFin: string;
  estado: 'activo' | 'finalizado' | 'pendiente';
}

@Component({
  selector: 'app-my-contracts',
  standalone: false,
  templateUrl: './my-contracts.html',
  styleUrl: './my-contracts.css'
})
export class MyContracts implements AfterViewInit {
 @Input() user: any;
 
  contratos: Contrato[] = [
    { id: 1, nombre: 'Contrato de Alquiler - Piso 1', fechaInicio: '2023-01-01', fechaFin: '2024-01-01', estado: 'activo' },
    { id: 2, nombre: 'Contrato de Alquiler - Local Comercial', fechaInicio: '2022-06-15', fechaFin: '2023-06-14', estado: 'finalizado' },
    { id: 3, nombre: 'Contrato de Alquiler - Oficina', fechaInicio: '2023-05-01', fechaFin: '2024-05-01', estado: 'pendiente' },
  ];

  contratoSeleccionado: Contrato | null = null;

  @ViewChild('contratoModal') contratoModal!: ElementRef;
  private modalInstance: any;

  ngAfterViewInit() {
    this.modalInstance = new bootstrap.Modal(this.contratoModal.nativeElement);

    // Limpia el contrato seleccionado al cerrar el modal
    this.contratoModal.nativeElement.addEventListener('hidden.bs.modal', () => {
      this.contratoSeleccionado = null;
    });
  }

  abrirContrato(contrato: Contrato) {
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
