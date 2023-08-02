import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditClientTypeComponent } from './edit-client-type.component';

describe('EditClientTypeComponent', () => {
  let component: EditClientTypeComponent;
  let fixture: ComponentFixture<EditClientTypeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditClientTypeComponent]
    });
    fixture = TestBed.createComponent(EditClientTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
