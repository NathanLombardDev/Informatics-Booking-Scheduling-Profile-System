import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateAccountDetailsComponent } from './update-account-details.component';

describe('UpdateAccountDetailsComponent', () => {
  let component: UpdateAccountDetailsComponent;
  let fixture: ComponentFixture<UpdateAccountDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UpdateAccountDetailsComponent]
    });
    fixture = TestBed.createComponent(UpdateAccountDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
