import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBicycleBrandComponent } from './add-bicycle-brand.component';

describe('AddBicycleBrandComponent', () => {
  let component: AddBicycleBrandComponent;
  let fixture: ComponentFixture<AddBicycleBrandComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddBicycleBrandComponent]
    });
    fixture = TestBed.createComponent(AddBicycleBrandComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
