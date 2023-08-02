import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditBicycleComponent } from './edit-bicycle.component';

describe('EditBicycleComponent', () => {
  let component: EditBicycleComponent;
  let fixture: ComponentFixture<EditBicycleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditBicycleComponent]
    });
    fixture = TestBed.createComponent(EditBicycleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
