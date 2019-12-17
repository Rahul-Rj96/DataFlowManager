import { TestBed } from '@angular/core/testing';

import { FormtypeService } from './formtype.service';

describe('FormtypeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FormtypeService = TestBed.get(FormtypeService);
    expect(service).toBeTruthy();
  });
});
