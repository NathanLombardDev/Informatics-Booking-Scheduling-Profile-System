import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuccessBookingPaymentComponent } from './success-booking-payment.component';

describe('SuccessBookingPaymentComponent', () => {
  let component: SuccessBookingPaymentComponent;
  let fixture: ComponentFixture<SuccessBookingPaymentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SuccessBookingPaymentComponent]
    });
    fixture = TestBed.createComponent(SuccessBookingPaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
