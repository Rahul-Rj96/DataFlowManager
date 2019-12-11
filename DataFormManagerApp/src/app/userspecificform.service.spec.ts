import { TestBed } from '@angular/core/testing';

import { UserSpecificFormsService } from './userspecificform.service';

describe('UserspecificformService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UserSpecificFormsService = TestBed.get(UserSpecificFormsService);
    expect(service).toBeTruthy();
  });
});
