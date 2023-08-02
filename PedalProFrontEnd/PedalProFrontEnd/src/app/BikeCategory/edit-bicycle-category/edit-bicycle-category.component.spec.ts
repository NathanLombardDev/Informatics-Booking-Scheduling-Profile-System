import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditBicycleCategoryComponent } from './edit-bicycle-category.component';

describe('EditBicycleCategoryComponent', () => {
  let component: EditBicycleCategoryComponent;
  let fixture: ComponentFixture<EditBicycleCategoryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditBicycleCategoryComponent]
    });
    fixture = TestBed.createComponent(EditBicycleCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
