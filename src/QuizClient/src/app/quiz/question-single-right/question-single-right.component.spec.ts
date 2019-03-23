import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MatRadioModule, MatButtonModule, MatIconModule } from '@angular/material';
import { SharedModule } from '../../shared/shared.module';
import { QuestionSingleRightComponent } from './question-single-right.component';
import { QuestionTemplate } from '../../codegen/model.g';
import IQuestionComponentData from '../IQuestionComponentData';

describe('QuestionSingleRightComponent', () => {
  let component: QuestionSingleRightComponent;
  let fixture: ComponentFixture<QuestionSingleRightComponent>;
  let questionComponentData: IQuestionComponentData;

  beforeEach(async(() => {
    questionComponentData = {
      component: 'SingleRight',
      inputs: {
        questionId: 1,
        questionTemplate: new QuestionTemplate()}
      };

    TestBed.configureTestingModule({
      imports: [
        MatRadioModule,
        MatButtonModule,
        MatIconModule,
        SharedModule
      ],
      declarations: [ QuestionSingleRightComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuestionSingleRightComponent);
    component = fixture.componentInstance;
    component.initData(questionComponentData);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
