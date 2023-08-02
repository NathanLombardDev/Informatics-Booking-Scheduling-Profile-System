import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EdittimeslotsComponent } from './edittimeslots.component';

describe('EdittimeslotsComponent', () => {
  let component: EdittimeslotsComponent;
  let fixture: ComponentFixture<EdittimeslotsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EdittimeslotsComponent]
    });
    fixture = TestBed.createComponent(EdittimeslotsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
