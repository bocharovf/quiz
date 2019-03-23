import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute } from '@angular/router';
import { MatRadioModule, MatButtonModule, MatIconModule } from '@angular/material';
import { EMPTY } from 'rxjs';
import { Observable } from 'rxjs/Observable';
import { SharedModule } from '../../shared/shared.module';
import { QuizFlowPageComponent } from './quiz-flow-page.component';
import { QuizFlowQuestionComponent } from '../quiz-flow-question/quiz-flow-question.component';
import { QuestionSingleRightComponent } from '../question-single-right/question-single-right.component';
import { QuizFlowService } from '../quiz-flow.service';

describe('QuizFlowPageComponent', () => {
  let component: QuizFlowPageComponent;
  let fixture: ComponentFixture<QuizFlowPageComponent>;

  const activatedRoute = {
    snapshot: {
      params: {
        id: '15'
      }
    }
  };
  beforeEach(async(() => {
    const activateQuiz = jasmine.createSpy('activateQuiz');
    const quizFlowService = {
      activateQuiz,
      quizCommand$: EMPTY,
      activeQuiz$: EMPTY
    };
    TestBed.configureTestingModule({
      imports: [
        MatRadioModule,
        MatButtonModule,
        MatIconModule,
        SharedModule,
        RouterTestingModule
      ],
      providers: [
        { provide: QuizFlowService, useValue: quizFlowService },
        { provide: ActivatedRoute, useValue: activatedRoute }
      ],
      declarations: [
        QuizFlowPageComponent,
        QuizFlowQuestionComponent,
        QuestionSingleRightComponent
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizFlowPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
