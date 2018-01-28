import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatRadioModule, MatButtonModule, MatIconModule } from '@angular/material';

import { SharedModule } from '../shared/shared.module';
import { QuizFlowDataService } from './quiz-flow-data.service';
import { QuizFlowService } from './quiz-flow.service';

import { QuizFlowPageComponent } from './quiz-flow-page/quiz-flow-page.component';
import { QuestionSingleRightComponent } from './question-single-right/question-single-right.component';
import { QuizFlowQuestionComponent } from './quiz-flow-question/quiz-flow-question.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    MatRadioModule,
    MatButtonModule,
    MatIconModule,
    SharedModule
  ],
  declarations: [
    QuizFlowPageComponent,
    QuestionSingleRightComponent,
    QuizFlowQuestionComponent
  ],
  providers: [ QuizFlowDataService, QuizFlowService ]
})
export class QuizModule { }
