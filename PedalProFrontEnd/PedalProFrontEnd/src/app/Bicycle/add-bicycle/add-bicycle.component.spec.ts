import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBicycleComponent } from './add-bicycle.component';

describe('AddBicycleComponent', () => {
  let component: AddBicycleComponent;
  let fixture: ComponentFixture<AddBicycleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddBicycleComponent]
    });
    fixture = TestBed.createComponent(AddBicycleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
