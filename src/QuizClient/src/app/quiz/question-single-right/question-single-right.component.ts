import { Component, OnInit } from '@angular/core';
import { Input } from '@angular/core';
import { Output } from '@angular/core';
import { Injector } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { MatRadioChange } from '@angular/material';

import { QuestionTemplate, QuizFlowCommandType, Answer } from '../../codegen/model.g';
import IQuestionComponent from '../IQuestionComponent';

/**
 * Displays question with choosing single right answer from multiple options.
 */
@Component({
  selector: 'quiz-question-single-right',
  templateUrl: './question-single-right.component.html',
  styleUrls: ['./question-single-right.component.scss']
})
export class QuestionSingleRightComponent implements IQuestionComponent, OnInit {
  questionId: number;
  questionTemplate: QuestionTemplate;

  @Output() answered = new EventEmitter<boolean>();

  private selectedAnswerId: number;

  constructor(private injector: Injector) {
    this.questionId = this.injector.get('questionId');
    this.questionTemplate = this.injector.get('questionTemplate');
  }

  ngOnInit() {

  }

  answerChanged(args: MatRadioChange) {
    this.selectedAnswerId = args.value;
    this.answered.emit(true);
  }

  getAnswer(): Answer {
    const answer = new Answer();
    answer.templateId = this.selectedAnswerId;
    return answer;
  }
}
