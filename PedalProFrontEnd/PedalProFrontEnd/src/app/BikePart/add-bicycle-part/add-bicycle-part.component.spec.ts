import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBicyclePartComponent } from './add-bicycle-part.component';

describe('AddBicyclePartComponent', () => {
  let component: AddBicyclePartComponent;
  let fixture: ComponentFixture<AddBicyclePartComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddBicyclePartComponent]
    });
    fixture = TestBed.createComponent(AddBicyclePartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
