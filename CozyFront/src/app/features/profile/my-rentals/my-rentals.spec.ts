import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyRentals } from './my-rentals';

describe('MyRentals', () => {
  let component: MyRentals;
  let fixture: ComponentFixture<MyRentals>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MyRentals]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MyRentals);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
