import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncidenceForm } from './incidence-form';

describe('IncidenceForm', () => {
  let component: IncidenceForm;
  let fixture: ComponentFixture<IncidenceForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [IncidenceForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IncidenceForm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
