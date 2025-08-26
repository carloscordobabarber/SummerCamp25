import { TestBed } from '@angular/core/testing';

import { ApartmentWorker } from './apartment-worker';

describe('ApartmentWorker', () => {
  let service: ApartmentWorker;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApartmentWorker);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
