import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PackageListReportComponent } from './package-list-report.component';

describe('PackageListReportComponent', () => {
  let component: PackageListReportComponent;
  let fixture: ComponentFixture<PackageListReportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PackageListReportComponent]
    });
    fixture = TestBed.createComponent(PackageListReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
