import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BicyclePartComponent } from './bicycle-part.component';

describe('BicyclePartComponent', () => {
  let component: BicyclePartComponent;
  let fixture: ComponentFixture<BicyclePartComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BicyclePartComponent]
    });
    fixture = TestBed.createComponent(BicyclePartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
