import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuccessCheckoutComponent } from './success-checkout.component';

describe('SuccessCheckoutComponent', () => {
  let component: SuccessCheckoutComponent;
  let fixture: ComponentFixture<SuccessCheckoutComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SuccessCheckoutComponent]
    });
    fixture = TestBed.createComponent(SuccessCheckoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
