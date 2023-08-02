import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyUploadIFComponent } from './company-upload-if.component';

describe('CompanyUploadIFComponent', () => {
  let component: CompanyUploadIFComponent;
  let fixture: ComponentFixture<CompanyUploadIFComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CompanyUploadIFComponent]
    });
    fixture = TestBed.createComponent(CompanyUploadIFComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
