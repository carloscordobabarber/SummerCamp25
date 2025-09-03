import { TestBed } from '@angular/core/testing';

import { ApartmentCardService } from './apartment-card';

describe('ApartmentCardService', () => {
  let service: ApartmentCardService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
  service = TestBed.inject(ApartmentCardService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
