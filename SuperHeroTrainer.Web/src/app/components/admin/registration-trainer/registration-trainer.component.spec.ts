import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrationTrainerComponent } from './registration-trainer.component';

describe('RegistrationTrainerComponent', () => {
  let component: RegistrationTrainerComponent;
  let fixture: ComponentFixture<RegistrationTrainerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegistrationTrainerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrationTrainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
