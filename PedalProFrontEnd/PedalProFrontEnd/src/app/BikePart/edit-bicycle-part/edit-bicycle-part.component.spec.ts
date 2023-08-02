import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditBicyclePartComponent } from './edit-bicycle-part.component';

describe('EditBicyclePartComponent', () => {
  let component: EditBicyclePartComponent;
  let fixture: ComponentFixture<EditBicyclePartComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditBicyclePartComponent]
    });
    fixture = TestBed.createComponent(EditBicyclePartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
