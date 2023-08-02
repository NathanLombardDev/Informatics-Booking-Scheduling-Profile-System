import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditHelpComponent } from './edit-help.component';

describe('EditHelpComponent', () => {
  let component: EditHelpComponent;
  let fixture: ComponentFixture<EditHelpComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditHelpComponent]
    });
    fixture = TestBed.createComponent(EditHelpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
