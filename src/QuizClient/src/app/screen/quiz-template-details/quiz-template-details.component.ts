import { Component, OnInit, Input } from '@angular/core';
import { QuizFlowService } from '../../quiz/quiz-flow.service';
import * as Model from '../../codegen/model.g';

@Component({
  selector: 'quiz-template-details',
  templateUrl: './quiz-template-details.component.html',
  styleUrls: ['./quiz-template-details.component.scss']
})
export class QuizTemplateDetailsComponent implements OnInit {
  @Input() quizTemplate: Model.QuizTemplate;

  private questionsAmount: number;

  constructor(private quizFlowService: QuizFlowService) { }

  ngOnInit() {
  }

  onStartNewQuiz() {
    this.quizFlowService.startNewQuiz(this.quizTemplate);
  }
}
