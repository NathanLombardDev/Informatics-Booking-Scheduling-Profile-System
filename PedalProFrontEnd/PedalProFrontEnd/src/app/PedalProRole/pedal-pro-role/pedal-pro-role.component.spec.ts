import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PedalProRoleComponent } from './pedal-pro-role.component';

describe('PedalProRoleComponent', () => {
  let component: PedalProRoleComponent;
  let fixture: ComponentFixture<PedalProRoleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PedalProRoleComponent]
    });
    fixture = TestBed.createComponent(PedalProRoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
