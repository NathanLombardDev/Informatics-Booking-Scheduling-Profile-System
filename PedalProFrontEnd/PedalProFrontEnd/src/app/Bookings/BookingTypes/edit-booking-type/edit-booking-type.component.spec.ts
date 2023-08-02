import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditBookingTypeComponent } from './edit-booking-type.component';

describe('EditBookingTypeComponent', () => {
  let component: EditBookingTypeComponent;
  let fixture: ComponentFixture<EditBookingTypeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditBookingTypeComponent]
    });
    fixture = TestBed.createComponent(EditBookingTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
