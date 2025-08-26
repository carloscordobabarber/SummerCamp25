import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncidenceViewer } from './incidence-viewer';

describe('IncidenceViewer', () => {
  let component: IncidenceViewer;
  let fixture: ComponentFixture<IncidenceViewer>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [IncidenceViewer]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IncidenceViewer);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
