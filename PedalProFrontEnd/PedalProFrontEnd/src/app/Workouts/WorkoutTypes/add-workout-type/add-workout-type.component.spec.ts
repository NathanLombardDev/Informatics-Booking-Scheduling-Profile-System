import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddWorkoutTypeComponent } from './add-workout-type.component';

describe('AddWorkoutTypeComponent', () => {
  let component: AddWorkoutTypeComponent;
  let fixture: ComponentFixture<AddWorkoutTypeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddWorkoutTypeComponent]
    });
    fixture = TestBed.createComponent(AddWorkoutTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
