import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MatToolbarModule, MatIconModule, MatSnackBarModule } from '@angular/material';
import { RouterTestingModule } from '@angular/router/testing';
import { QuizToolbarComponent } from './quiz-toolbar.component';
import { NavigationService } from '../navigation.service';

describe('QuizToolbar', () => {
  let component: QuizToolbarComponent;
  let fixture: ComponentFixture<QuizToolbarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        MatToolbarModule,
        MatIconModule,
        MatSnackBarModule
      ],
      providers: [
        { provide: NavigationService }
      ],
      declarations: [ QuizToolbarComponent ]
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
