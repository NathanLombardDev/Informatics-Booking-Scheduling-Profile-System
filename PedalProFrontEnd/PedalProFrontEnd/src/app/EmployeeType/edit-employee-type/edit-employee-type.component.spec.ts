import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditEmployeeTypeComponent } from './edit-employee-type.component';

describe('EditEmployeeTypeComponent', () => {
  let component: EditEmployeeTypeComponent;
  let fixture: ComponentFixture<EditEmployeeTypeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditEmployeeTypeComponent]
    });
    fixture = TestBed.createComponent(EditEmployeeTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
