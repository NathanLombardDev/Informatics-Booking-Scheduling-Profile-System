import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaffReportComponent } from './staff-report.component';

describe('StaffReportComponent', () => {
  let component: StaffReportComponent;
  let fixture: ComponentFixture<StaffReportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [StaffReportComponent]
    });
    fixture = TestBed.createComponent(StaffReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
