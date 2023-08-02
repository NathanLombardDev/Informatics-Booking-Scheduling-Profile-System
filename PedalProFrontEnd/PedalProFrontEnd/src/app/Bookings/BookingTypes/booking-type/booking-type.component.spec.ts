import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookingTypeComponent } from './booking-type.component';

describe('BookingTypeComponent', () => {
  let component: BookingTypeComponent;
  let fixture: ComponentFixture<BookingTypeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BookingTypeComponent]
    });
    fixture = TestBed.createComponent(BookingTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
