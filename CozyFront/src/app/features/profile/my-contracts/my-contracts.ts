import { AfterViewInit, Component, ElementRef, Input, ViewChild, OnInit } from '@angular/core';
import { UserService } from '../../../services/user/user.service';
import { UserRental } from '../../../models/user-rental';
import { UserRentalsService } from '../../../services/user/user-rentals.service';
import jsPDF from 'jspdf';

declare var bootstrap: any;

@Component({
  selector: 'app-my-contracts',
  standalone: false,
  templateUrl: './my-contracts.html',
  styleUrl: './my-contracts.css'
})
export class MyContracts implements AfterViewInit, OnInit {
  @Input() user: any;
  contracts: UserRental[] = [];
  selectedContract: UserRental | null = null;
  @ViewChild('contratoModal') contratoModal!: ElementRef;
  private modalInstance: any;

  constructor(private userService: UserRentalsService, private userProfileService: UserService) { }

  ngOnInit(): void {
    const userId = this.userProfileService.getUserIdFromToken();
    if (userId) {
      this.userService.getRentalsByUserId(userId).subscribe({
        next: (data) => this.contracts = data,
        error: (err) => console.error('Error al cargar contratos', err)
      });
    }
  }

  ngAfterViewInit() {
    this.modalInstance = new bootstrap.Modal(this.contratoModal.nativeElement);
    // Limpia el contract seleccionado al cerrar el modal
    this.contratoModal.nativeElement.addEventListener('hidden.bs.modal', () => {
      this.selectedContract = null;
    });
  }

  openContract(contract: UserRental) {
    this.selectedContract = { ...contract }; //contract;
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

  closeContract() {
    this.modalInstance.hide();
  }

  getContractStatus(contract: UserRental): string {
    const today = new Date();
    const start = new Date(contract.startDate);
    const end = new Date(contract.endDate);

    // Si today < start, debe ser 'I' (Pendiente)
    if (today < start) {
      if (contract.statusId !== 'B') {
        // Actualizar en la BD si es necesario
        this.userService.updateContractStatus(contract.rentalId, 'B').subscribe({
          next: () => contract.statusId = 'B',
          error: (err: any) => console.error('Error actualizando statusId a B', err)
        });
      }
    }
    // Si today > end, debe ser 'B' (Finalizado)
    else if (today > end) {
      if (contract.statusId !== 'I') {
        this.userService.updateContractStatus(contract.rentalId, 'I').subscribe({
          next: () => contract.statusId = 'I',
          error: (err: any) => console.error('Error actualizando statusId a I', err)
        });
      }
    }
    // Si está activo, debe ser 'A'
    else {
      if (contract.statusId !== 'A') {
        this.userService.updateContractStatus(contract.rentalId, 'A').subscribe({
          next: () => contract.statusId = 'A',
          error: (err: any) => console.error('Error actualizando statusId a A', err)
        });
      }
    }
    return contract.statusName;
  }

  isContractActive(contract: UserRental): boolean {
    const today = new Date();
    const start = new Date(contract.startDate);
    const end = new Date(contract.endDate);
    return today >= start && today <= end;
  }

  isContractFinished(contract: UserRental): boolean {
    const today = new Date();
    const end = new Date(contract.endDate);
    return today > end;
  }

  isContractPending(contract: UserRental): boolean {
    const today = new Date();
    const start = new Date(contract.startDate);
    return today < start;
  }

  downloadPdf(contract: UserRental | null) {
    if (!contract) return;
    // const doc = new jsPDF();
    // const title = `Contrato de Alquiler - ${contract.streetName}${contract.portal ? contract.portal : ''}, ${contract.apartmentFloor}${contract.apartmentDoor ? contract.apartmentDoor : ''}`;
    // doc.setFont('times', 'normal');
    // doc.setFontSize(16);
    // doc.text(title, 10, 20);
    // doc.setFontSize(12);
    // doc.text(`Fecha Inicio: ${new Date(contract.startDate).toLocaleDateString()}`, 10, 30);
    // doc.text(`Fecha Fin: ${new Date(contract.endDate).toLocaleDateString()}`, 10, 40);
    // doc.text(`Estado: ${this.getEstadoContrato(contract)}`, 10, 50);
    // doc.text('Por medio del presente contrato, el arrendatario y el arrendador, CozyHouse, acuerdan lo siguiente:', 10, 60);
    // doc.text('1. Objeto del Contrato: El arrendador concede en alquiler el inmueble para uso residencial/comercial.', 10, 70);
    // doc.text('2. Duración: El presente contrato tendrá la duración indicada arriba.', 10, 80);
    // doc.text('3. Pago: El arrendatario se compromete a realizar los pagos mensuales en las fechas establecidas.', 10, 90);
    // doc.text('4. Obligaciones: Ambas partes se comprometen a respetar y cumplir con las cláusulas estipuladas.', 10, 100);
    // doc.text('___________________________        ___________________________', 10, 120);
    // doc.text('Arrendatario                                Arrendador', 10, 130);
    // doc.save(`${title}.pdf`);

    const doc = new jsPDF();
    const pageWidth = doc.internal.pageSize.getWidth();

    // Margen lateral
    const marginX = 20;
    let cursorY = 20;

    // 1. Título centrado
    const title = `Contrato de Alquiler - ${contract.streetName}${contract.portal ?? ''}, ${contract.apartmentFloor}${contract.apartmentDoor ?? ''}`;
    doc.setFont("times", "bold");
    doc.setFontSize(16);
    const titleWidth = doc.getTextWidth(title);
    doc.text(title, (pageWidth - titleWidth) / 2, cursorY);
    cursorY += 10;

    // 2. Línea decorativa
    doc.setDrawColor(150);
    doc.setLineWidth(0.5);
    doc.line(marginX, cursorY, pageWidth - marginX, cursorY);
    cursorY += 10;

    // 3. Datos del contrato (fechas, estado)
    doc.setFont("times", "normal");
    doc.setFontSize(12);
    doc.text(`Fecha de Inicio: ${new Date(contract.startDate).toLocaleDateString()}`, marginX, cursorY);
    cursorY += 7;
    doc.text(`Fecha de Fin: ${new Date(contract.endDate).toLocaleDateString()}`, marginX, cursorY);
    cursorY += 7;
    doc.text(`Estado: ${this.getContractStatus(contract)}`, marginX, cursorY);
    cursorY += 15;

    // 4. Texto principal (párrafos con salto de línea y justificado)
    const bodyText = [
      'Por medio del presente contrato, el arrendatario y el arrendador, CozyHouse, acuerdan lo siguiente:',
      '',
      '1. Objeto del Contrato: El arrendador concede en alquiler el inmueble para uso residencial/comercial.',
      '2. Duración: El presente contrato tendrá la duración indicada arriba.',
      '3. Pago: El arrendatario se compromete a realizar los pagos mensuales en las fechas establecidas.',
      '4. Obligaciones: Ambas partes se comprometen a respetar y cumplir con las cláusulas estipuladas.'
    ];

    bodyText.forEach(paragraph => {
      const lines = doc.splitTextToSize(paragraph, pageWidth - 2 * marginX);
      doc.text(lines, marginX, cursorY);
      cursorY += lines.length * 7 + 5;
    });

    // 5. Firmas
    cursorY += 20;
    const sigWidth = 50;
    doc.line(marginX, cursorY, marginX + sigWidth, cursorY);
    doc.line(pageWidth - marginX - sigWidth, cursorY, pageWidth - marginX, cursorY);
    cursorY += 7;
    doc.text("Arrendatario", marginX + 10, cursorY);
    doc.text("Arrendador", pageWidth - marginX - sigWidth + 10, cursorY);

    // 6. Pie de página
    const footerText = "CozyHouse · www.cozyhouse.com · contacto@cozyhouse.com";
    doc.setFontSize(10);
    doc.setTextColor(150);
    doc.text(footerText, pageWidth / 2, doc.internal.pageSize.getHeight() - 10, { align: "center" });

    // Guardar archivo
    const safeTitle = title.replace(/[^\w\s\-]/g, "_");
    doc.save(`${safeTitle}.pdf`);
  }
}
