import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ExaminingComponent } from './examining.component';

describe('ExaminingComponent', () => {
  let component: ExaminingComponent;
  let fixture: ComponentFixture<ExaminingComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ExaminingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExaminingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
