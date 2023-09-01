import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopularDaysReportComponent } from './popular-days-report.component';

describe('PopularDaysReportComponent', () => {
  let component: PopularDaysReportComponent;
  let fixture: ComponentFixture<PopularDaysReportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PopularDaysReportComponent]
    });
    fixture = TestBed.createComponent(PopularDaysReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
