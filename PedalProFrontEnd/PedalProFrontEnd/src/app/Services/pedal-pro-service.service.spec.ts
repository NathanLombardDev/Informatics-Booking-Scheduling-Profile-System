import { TestBed } from '@angular/core/testing';

import { PedalProServiceService } from './pedal-pro-service.service';

describe('PedalProServiceService', () => {
  let service: PedalProServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PedalProServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
