import { TestBed } from '@angular/core/testing';

import { UserspecificformService } from './userspecificform.service';

describe('UserspecificformService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UserspecificformService = TestBed.get(UserspecificformService);
    expect(service).toBeTruthy();
  });
});
