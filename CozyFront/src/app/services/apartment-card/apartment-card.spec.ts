import { TestBed } from '@angular/core/testing';

import { ApartmentCard } from './apartment-card';

describe('ApartmentCard', () => {
  let service: ApartmentCard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApartmentCard);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
