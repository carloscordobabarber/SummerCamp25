import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApartmentDetails } from './apartment-details';

describe('ApartmentDetails', () => {
  let component: ApartmentDetails;
  let fixture: ComponentFixture<ApartmentDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ApartmentDetails]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ApartmentDetails);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
