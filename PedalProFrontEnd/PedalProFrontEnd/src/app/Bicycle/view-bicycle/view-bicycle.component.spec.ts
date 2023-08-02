import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewBicycleComponent } from './view-bicycle.component';

describe('ViewBicycleComponent', () => {
  let component: ViewBicycleComponent;
  let fixture: ComponentFixture<ViewBicycleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ViewBicycleComponent]
    });
    fixture = TestBed.createComponent(ViewBicycleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
