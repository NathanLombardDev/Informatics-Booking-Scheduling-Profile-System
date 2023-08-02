import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingModuleCompanyComponent } from './training-module-company.component';

describe('TrainingModuleCompanyComponent', () => {
  let component: TrainingModuleCompanyComponent;
  let fixture: ComponentFixture<TrainingModuleCompanyComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TrainingModuleCompanyComponent]
    });
    fixture = TestBed.createComponent(TrainingModuleCompanyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
