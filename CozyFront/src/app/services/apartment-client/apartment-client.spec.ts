import { TestBed } from '@angular/core/testing';

import { ApartmentClientsService } from './apartment-client';

describe('ApartmentClientsService', () => {
  let service: ApartmentClientsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApartmentClientsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
