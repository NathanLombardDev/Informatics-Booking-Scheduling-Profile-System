import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnsuccessBookingPaymentComponent } from './unsuccess-booking-payment.component';

describe('UnsuccessBookingPaymentComponent', () => {
  let component: UnsuccessBookingPaymentComponent;
  let fixture: ComponentFixture<UnsuccessBookingPaymentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UnsuccessBookingPaymentComponent]
    });
    fixture = TestBed.createComponent(UnsuccessBookingPaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
