import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewHelpCompanyComponent } from './view-help-company.component';

describe('ViewHelpCompanyComponent', () => {
  let component: ViewHelpCompanyComponent;
  let fixture: ComponentFixture<ViewHelpCompanyComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ViewHelpCompanyComponent]
    });
    fixture = TestBed.createComponent(ViewHelpCompanyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
