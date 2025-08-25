import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncidenceList } from './incidence-list';

describe('IncidenceList', () => {
  let component: IncidenceList;
  let fixture: ComponentFixture<IncidenceList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [IncidenceList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IncidenceList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
