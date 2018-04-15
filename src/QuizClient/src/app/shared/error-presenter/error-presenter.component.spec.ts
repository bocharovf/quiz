import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ErrorPresenterComponent } from './error-presenter.component';
import { MatListModule, MatIconModule } from '@angular/material';

describe('ErrorPresenterComponent', () => {
  let component: ErrorPresenterComponent;
  let fixture: ComponentFixture<ErrorPresenterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [MatListModule, MatIconModule],
      declarations: [ ErrorPresenterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ErrorPresenterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
