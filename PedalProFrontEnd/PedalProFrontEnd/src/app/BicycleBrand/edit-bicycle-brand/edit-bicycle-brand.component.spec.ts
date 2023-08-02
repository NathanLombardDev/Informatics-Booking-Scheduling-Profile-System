import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditBicycleBrandComponent } from './edit-bicycle-brand.component';

describe('EditBicycleBrandComponent', () => {
  let component: EditBicycleBrandComponent;
  let fixture: ComponentFixture<EditBicycleBrandComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditBicycleBrandComponent]
    });
    fixture = TestBed.createComponent(EditBicycleBrandComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
