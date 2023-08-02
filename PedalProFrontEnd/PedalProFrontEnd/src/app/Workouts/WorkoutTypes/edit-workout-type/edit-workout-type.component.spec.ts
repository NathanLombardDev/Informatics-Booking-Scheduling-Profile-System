import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditWorkoutTypeComponent } from './edit-workout-type.component';

describe('EditWorkoutTypeComponent', () => {
  let component: EditWorkoutTypeComponent;
  let fixture: ComponentFixture<EditWorkoutTypeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditWorkoutTypeComponent]
    });
    fixture = TestBed.createComponent(EditWorkoutTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
