import { TestBed } from '@angular/core/testing';

import { ApiAptCli } from './apartment-client';

describe('ApiAptCli', () => {
  let service: ApiAptCli;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiAptCli);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
