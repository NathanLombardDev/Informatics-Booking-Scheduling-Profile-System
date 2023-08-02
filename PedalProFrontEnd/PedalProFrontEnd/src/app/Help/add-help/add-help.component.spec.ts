import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddHelpComponent } from './add-help.component';

describe('AddHelpComponent', () => {
  let component: AddHelpComponent;
  let fixture: ComponentFixture<AddHelpComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddHelpComponent]
    });
    fixture = TestBed.createComponent(AddHelpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
