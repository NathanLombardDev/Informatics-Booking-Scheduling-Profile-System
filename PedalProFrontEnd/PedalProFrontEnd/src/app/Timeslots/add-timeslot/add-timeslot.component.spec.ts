import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTimeslotComponent } from './add-timeslot.component';

describe('AddTimeslotComponent', () => {
  let component: AddTimeslotComponent;
  let fixture: ComponentFixture<AddTimeslotComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddTimeslotComponent]
    });
    fixture = TestBed.createComponent(AddTimeslotComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
