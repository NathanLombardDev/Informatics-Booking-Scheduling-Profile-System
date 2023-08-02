import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BicycleCategoryComponent } from './bicycle-category.component';

describe('BicycleCategoryComponent', () => {
  let component: BicycleCategoryComponent;
  let fixture: ComponentFixture<BicycleCategoryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BicycleCategoryComponent]
    });
    fixture = TestBed.createComponent(BicycleCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
