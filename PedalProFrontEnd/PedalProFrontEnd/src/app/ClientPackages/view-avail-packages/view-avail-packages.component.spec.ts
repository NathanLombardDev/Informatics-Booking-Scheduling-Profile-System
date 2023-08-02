import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewAvailPackagesComponent } from './view-avail-packages.component';

describe('ViewAvailPackagesComponent', () => {
  let component: ViewAvailPackagesComponent;
  let fixture: ComponentFixture<ViewAvailPackagesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ViewAvailPackagesComponent]
    });
    fixture = TestBed.createComponent(ViewAvailPackagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
