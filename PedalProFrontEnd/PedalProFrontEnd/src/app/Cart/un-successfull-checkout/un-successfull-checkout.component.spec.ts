import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnSuccessfullCheckoutComponent } from './un-successfull-checkout.component';

describe('UnSuccessfullCheckoutComponent', () => {
  let component: UnSuccessfullCheckoutComponent;
  let fixture: ComponentFixture<UnSuccessfullCheckoutComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UnSuccessfullCheckoutComponent]
    });
    fixture = TestBed.createComponent(UnSuccessfullCheckoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
