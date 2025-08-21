import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardManager } from './card-manager';

describe('CardManager', () => {
  let component: CardManager;
  let fixture: ComponentFixture<CardManager>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CardManager]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CardManager);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
