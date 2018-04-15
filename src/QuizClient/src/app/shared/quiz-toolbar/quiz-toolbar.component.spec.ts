import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MatToolbarModule, MatIconModule, MatSnackBarModule } from '@angular/material';
import { RouterTestingModule } from '@angular/router/testing';

import { AuthModule } from '../../auth/auth.module';
import { QuizToolbarComponent } from './quiz-toolbar.component';
import { NavigationService } from '../navigation.service';
import { AuthService } from '../../auth/auth.service';
import { SharedModule } from '../shared.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('QuizToolbar', () => {
  let component: QuizToolbarComponent;
  let fixture: ComponentFixture<QuizToolbarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        MatToolbarModule,
        MatIconModule,
        MatSnackBarModule,
        AuthModule,
        SharedModule
      ],
      providers: [
        NavigationService,
        AuthService
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizToolbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
