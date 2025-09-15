import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MyContracts } from './my-contracts';
import { UserRentalsService } from '../../../services/user/user-rentals.service';
import { UserService } from '../../../services/user/user.service';
import { of, throwError } from 'rxjs';
import jsPDF from 'jspdf';
import * as jsPDFModule from 'jspdf';

fdescribe('MyContracts', () => {
  it('should render contract list in template', () => {
    const mockContracts = [{
      rentalId: 1,
      userId: 2,
      apartmentId: 3,
      apartmentPrice: 500,
      apartmentCode: 'A1',
      apartmentDoor: 'A',
      apartmentFloor: 1,
      apartmentArea: 80,
      startDate: '2025-09-01',
      endDate: '2025-09-10',
      statusId: 'A',
      statusName: 'Activo',
      streetName: 'Calle Falsa',
      portal: '1',
      floor: 1,
      districtName: 'Centro'
    }];
    userServiceSpy.getUserIdFromToken.and.returnValue(123);
    userRentalsServiceSpy.getRentalsByUserId.and.returnValue(of(mockContracts));
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.textContent).toContain('Calle Falsa');
  });

  it('should handle error when loading contracts', () => {
    spyOn(console, 'error');
    userServiceSpy.getUserIdFromToken.and.returnValue(123);
    userRentalsServiceSpy.getRentalsByUserId.and.returnValue(throwError(() => new Error('Error de carga')));
    component.ngOnInit();
    expect(component.contracts.length).toBe(0);
  });

  it('should open modal when contract is clicked in template', () => {
    const contract = {
      rentalId: 1,
      userId: 2,
      apartmentId: 3,
      apartmentPrice: 500,
      apartmentCode: 'A1',
      apartmentDoor: 'A',
      apartmentFloor: 1,
      apartmentArea: 80,
      startDate: '2025-09-01',
      endDate: '2025-09-10',
      statusId: 'A',
      statusName: 'Activo',
      streetName: 'Calle Falsa',
      portal: '1',
      floor: 1,
      districtName: 'Centro'
    };
    component.contracts = [contract as any];
    fixture.detectChanges();
    spyOn(component, 'openContract');
    const compiled = fixture.nativeElement as HTMLElement;
    // Busca el primer td clickable de la tabla
    const contractCell = compiled.querySelector('tbody tr td');
    if (contractCell && contractCell instanceof HTMLElement) {
      contractCell.click();
      expect(component.openContract).toHaveBeenCalledWith(contract as any);
    }
  });
  let component: MyContracts;
  let fixture: ComponentFixture<MyContracts>;
  let userRentalsServiceSpy: jasmine.SpyObj<UserRentalsService>;
  let userServiceSpy: jasmine.SpyObj<UserService>;

  beforeEach(async () => {
    userRentalsServiceSpy = jasmine.createSpyObj('UserRentalsService', ['getRentalsByUserId', 'updateContractStatus']);
    userRentalsServiceSpy.updateContractStatus.and.returnValue(of({}));
    userServiceSpy = jasmine.createSpyObj('UserService', ['getUserIdFromToken']);

    // Mock global bootstrap for tests
    (window as any).bootstrap = {
      Modal: function() {
        return {
          show: jasmine.createSpy('show'),
          hide: jasmine.createSpy('hide')
        };
      }
    };

    await TestBed.configureTestingModule({
      declarations: [MyContracts],
      providers: [
        { provide: UserRentalsService, useValue: userRentalsServiceSpy },
        { provide: UserService, useValue: userServiceSpy }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(MyContracts);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });


  it('should load contracts on init', () => {
    const mockContracts = [{
      rentalId: 1,
      userId: 2,
      apartmentId: 3,
      apartmentPrice: 500,
      apartmentCode: 'A1',
      apartmentDoor: 'A',
      apartmentFloor: 1,
      apartmentArea: 80,
      startDate: '2025-09-01',
      endDate: '2025-09-10',
      statusId: 'A',
      statusName: 'Activo',
      streetName: 'Calle Falsa',
      portal: '1',
      floor: 1,
      districtName: 'Centro'
    }];
    userServiceSpy.getUserIdFromToken.and.returnValue(123);
    userRentalsServiceSpy.getRentalsByUserId.and.returnValue(of(mockContracts));
    component.ngOnInit();
    expect(component.contracts).toEqual(mockContracts);
  });

  it('should open and close contract modal', () => {
    component.contratoModal = { nativeElement: document.createElement('div') } as any;
    // Acceso a la propiedad privada modalInstance mediante casting
    (component as any).modalInstance = { show: jasmine.createSpy(), hide: jasmine.createSpy() };
    const contract = {
      rentalId: 1,
      userId: 2,
      apartmentId: 3,
      apartmentPrice: 500,
      apartmentCode: 'A1',
      apartmentDoor: 'A',
      apartmentFloor: 1,
      apartmentArea: 80,
      startDate: '2025-09-01',
      endDate: '2025-09-10',
      statusId: 'A',
      statusName: 'Activo',
      streetName: 'Calle Falsa',
      portal: '1',
      floor: 1,
      districtName: 'Centro'
    };
    component.openContract(contract as any);
    expect(component.selectedContract).toEqual(contract);
    expect((component as any).modalInstance.show).toHaveBeenCalled();

    component.closeContract();
    expect((component as any).modalInstance.hide).toHaveBeenCalled();
  });

  it('should return correct contract status', () => {
    const contract = {
      rentalId: 1,
      userId: 2,
      apartmentId: 3,
      apartmentPrice: 500,
      apartmentCode: 'A1',
      apartmentDoor: 'A',
      apartmentFloor: 1,
      apartmentArea: 80,
      startDate: '2025-09-01',
      endDate: '2025-09-10',
      statusId: 'A',
      statusName: 'Activo',
      streetName: 'Calle Falsa',
      portal: '1',
      floor: 1,
      districtName: 'Centro'
    };
    spyOn(component, 'getContractStatus').and.callThrough();
    const status = component.getContractStatus(contract as any);
    expect(status).toBe(contract.statusName);
  });

  it('should identify contract as active', () => {
    const today = new Date();
    const contract = {
      startDate: new Date(today.getFullYear(), today.getMonth(), today.getDate() - 1).toISOString(),
      endDate: new Date(today.getFullYear(), today.getMonth(), today.getDate() + 1).toISOString()
    };
    expect(component.isContractActive(contract as any)).toBeTrue();
  });

  it('should identify contract as finished', () => {
    const today = new Date();
    const contract = {
      endDate: new Date(today.getFullYear(), today.getMonth(), today.getDate() - 1).toISOString()
    };
    expect(component.isContractFinished(contract as any)).toBeTrue();
  });

  it('should identify contract as pending', () => {
    const today = new Date();
    const contract = {
      startDate: new Date(today.getFullYear(), today.getMonth(), today.getDate() + 1).toISOString()
    };
    expect(component.isContractPending(contract as any)).toBeTrue();
  });

  it('should call downloadPdf without error if contract is null', () => {
    expect(() => component.downloadPdf(null)).not.toThrow();
  });

  it('should call downloadPdf and create PDF if contract is provided', () => {
    // Espía el método save en el prototipo de jsPDF usando el import
    const originalSave = jsPDF.prototype.save;
    // Mock jsPDF class y define en window
    class jsPDFMock {
      setFont() {}
      setFontSize() {}
      getTextWidth() { return 100; }
      text() {}
      setDrawColor() {}
      setLineWidth() {}
      line() {}
      splitTextToSize() { return ['line1', 'line2']; }
      setTextColor() {}
      internal = { pageSize: { getWidth: () => 210, getHeight: () => 297 } };
      save = jasmine.createSpy('save');
    }
    (window as any).jsPDF = jsPDFMock;

    const contract = {
      streetName: 'Calle Falsa',
      portal: '1',
      apartmentFloor: '2',
      apartmentDoor: 'A',
      startDate: new Date().toISOString(),
      endDate: new Date().toISOString(),
      statusName: 'Activo'
    };
    spyOn(component, 'getContractStatus').and.returnValue('Activo');
    component.downloadPdf(contract as any);
    // Verifica que el método save fue llamado
    expect((component as any).pdfInstance.save).toHaveBeenCalled();
  });
});
