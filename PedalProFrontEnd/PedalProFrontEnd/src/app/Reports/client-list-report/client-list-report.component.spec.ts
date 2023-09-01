import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientListReportComponent } from './client-list-report.component';

describe('ClientListReportComponent', () => {
  let component: ClientListReportComponent;
  let fixture: ComponentFixture<ClientListReportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ClientListReportComponent]
    });
    fixture = TestBed.createComponent(ClientListReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
