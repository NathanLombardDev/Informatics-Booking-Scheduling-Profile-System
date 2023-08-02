import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBicycleCategoryComponent } from './add-bicycle-category.component';

describe('AddBicycleCategoryComponent', () => {
  let component: AddBicycleCategoryComponent;
  let fixture: ComponentFixture<AddBicycleCategoryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddBicycleCategoryComponent]
    });
    fixture = TestBed.createComponent(AddBicycleCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
