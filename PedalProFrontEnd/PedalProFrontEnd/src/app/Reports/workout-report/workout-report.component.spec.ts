import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkoutReportComponent } from './workout-report.component';

describe('WorkoutReportComponent', () => {
  let component: WorkoutReportComponent;
  let fixture: ComponentFixture<WorkoutReportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WorkoutReportComponent]
    });
    fixture = TestBed.createComponent(WorkoutReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
