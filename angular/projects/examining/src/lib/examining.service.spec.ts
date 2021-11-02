import { TestBed } from '@angular/core/testing';

import { ExaminingService } from './examining.service';

describe('ExaminingService', () => {
  let service: ExaminingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ExaminingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
