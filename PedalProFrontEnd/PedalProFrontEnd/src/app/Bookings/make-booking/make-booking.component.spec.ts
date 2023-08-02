import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MakeBookingComponent } from './make-booking.component';

describe('MakeBookingComponent', () => {
  let component: MakeBookingComponent;
  let fixture: ComponentFixture<MakeBookingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MakeBookingComponent]
    });
    fixture = TestBed.createComponent(MakeBookingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
