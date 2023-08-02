import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewHelpComponent } from './view-help.component';

describe('ViewHelpComponent', () => {
  let component: ViewHelpComponent;
  let fixture: ComponentFixture<ViewHelpComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ViewHelpComponent]
    });
    fixture = TestBed.createComponent(ViewHelpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
