import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Incidences } from './incidences';

describe('Incidences', () => {
  let component: Incidences;
  let fixture: ComponentFixture<Incidences>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [Incidences]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Incidences);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
