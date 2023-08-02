import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrintViewIFComponent } from './print-view-if.component';

describe('PrintViewIFComponent', () => {
  let component: PrintViewIFComponent;
  let fixture: ComponentFixture<PrintViewIFComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PrintViewIFComponent]
    });
    fixture = TestBed.createComponent(PrintViewIFComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
