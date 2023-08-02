import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientUploadIFComponent } from './client-upload-if.component';

describe('ClientUploadIFComponent', () => {
  let component: ClientUploadIFComponent;
  let fixture: ComponentFixture<ClientUploadIFComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ClientUploadIFComponent]
    });
    fixture = TestBed.createComponent(ClientUploadIFComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
