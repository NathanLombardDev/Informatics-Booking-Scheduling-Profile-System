import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewtimeslotsComponent } from './viewtimeslots.component';

describe('ViewtimeslotsComponent', () => {
  let component: ViewtimeslotsComponent;
  let fixture: ComponentFixture<ViewtimeslotsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ViewtimeslotsComponent]
    });
    fixture = TestBed.createComponent(ViewtimeslotsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
