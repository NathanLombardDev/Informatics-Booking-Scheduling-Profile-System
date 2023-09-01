import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestingTestingUploadComponent } from './testing-testing-upload.component';

describe('TestingTestingUploadComponent', () => {
  let component: TestingTestingUploadComponent;
  let fixture: ComponentFixture<TestingTestingUploadComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TestingTestingUploadComponent]
    });
    fixture = TestBed.createComponent(TestingTestingUploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
