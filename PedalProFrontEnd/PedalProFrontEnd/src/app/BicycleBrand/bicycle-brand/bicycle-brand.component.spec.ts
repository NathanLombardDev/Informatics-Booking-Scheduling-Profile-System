import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BicycleBrandComponent } from './bicycle-brand.component';

describe('BicycleBrandComponent', () => {
  let component: BicycleBrandComponent;
  let fixture: ComponentFixture<BicycleBrandComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BicycleBrandComponent]
    });
    fixture = TestBed.createComponent(BicycleBrandComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
