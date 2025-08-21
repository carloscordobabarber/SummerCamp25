import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Paginador } from './paginador';

describe('Paginador', () => {
  let component: Paginador;
  let fixture: ComponentFixture<Paginador>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [Paginador]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Paginador);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
