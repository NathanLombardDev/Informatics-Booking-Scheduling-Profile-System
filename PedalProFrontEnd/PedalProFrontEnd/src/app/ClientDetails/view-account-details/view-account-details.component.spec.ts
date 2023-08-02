import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewAccountDetailsComponent } from './view-account-details.component';

describe('ViewAccountDetailsComponent', () => {
  let component: ViewAccountDetailsComponent;
  let fixture: ComponentFixture<ViewAccountDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ViewAccountDetailsComponent]
    });
    fixture = TestBed.createComponent(ViewAccountDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
