import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MatRadioModule, MatButtonModule, MatIconModule } from '@angular/material';
import { SharedModule } from '../../shared/shared.module';
import { QuizFlowQuestionComponent } from './quiz-flow-question.component';
import { QuestionSingleRightComponent } from '../question-single-right/question-single-right.component';

describe('QuizFlowQuestionComponent', () => {
  let component: QuizFlowQuestionComponent;
  let fixture: ComponentFixture<QuizFlowQuestionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        MatRadioModule,
        MatButtonModule,
        MatIconModule,
        SharedModule
      ],
      declarations: [
        QuizFlowQuestionComponent,
        QuestionSingleRightComponent
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizFlowQuestionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
