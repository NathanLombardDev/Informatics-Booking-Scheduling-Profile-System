import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEmployeeTypeComponent } from './add-employee-type.component';

describe('AddEmployeeTypeComponent', () => {
  let component: AddEmployeeTypeComponent;
  let fixture: ComponentFixture<AddEmployeeTypeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddEmployeeTypeComponent]
    });
    fixture = TestBed.createComponent(AddEmployeeTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
