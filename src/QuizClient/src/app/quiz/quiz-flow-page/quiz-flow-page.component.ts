import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';

import { QuestionSingleRightComponent } from '../question-single-right/question-single-right.component';
import { QuizFlowService } from '../quiz-flow.service';
import { NavigationService } from '../../shared/navigation.service';
import IQuestionComponentData from '../IQuestionComponentData';
import * as Model from '../../codegen/model.g';

/**
 * Quiz flow page.
 */
@Component({
  selector: 'quiz-quiz-flow-page',
  templateUrl: './quiz-flow-page.component.html',
  styleUrls: ['./quiz-flow-page.component.scss']
})
export class QuizFlowPageComponent implements OnInit, OnDestroy {

  /** Current question component data. */
  currentQuestion: IQuestionComponentData;

  /** Error message to display. */
  errorMessage: string;

  /** Link to home location. */
  homeLink: string;

  private quizId: number;
  private commandSub: Subscription;
  private quizSub: Subscription;
  private readonly QUIZ_COMPLETED_ERROR = 'Quiz already completed.';

  constructor(
      private quizFlowService: QuizFlowService,
      private route: ActivatedRoute,
      private navigation: NavigationService) {
    this.quizId = +this.route.snapshot.params['id'];
    this.homeLink = '/' + NavigationService.homeRoute;
  }

  ngOnInit() {
    this.commandSub = this.quizFlowService
                          .quizCommand$.subscribe(command => this.onNextQuestion(command));

    this.quizSub = this.quizFlowService
                       .activeQuiz$.subscribe(quiz => this.onQuizActivated(quiz));

    this.quizFlowService.activateQuiz(this.quizId);
  }

  ngOnDestroy() {
    this.commandSub.unsubscribe();
    this.quizSub.unsubscribe();
  }

  /**
   * Handles question answer event.
   * @param answer The answer.
   */
  questionAnswered(answer: Model.Answer) {
    const questionId = this.currentQuestion.inputs.questionId;
    this.quizFlowService
        .answerQuestion(questionId, answer);
  }

  private onQuizActivated(quiz: Model.Quiz) {
    if (quiz.isCompleted) {
      this.errorMessage = this.QUIZ_COMPLETED_ERROR;
      return;
    }

    if (quiz.id === this.quizId) {
      this.quizFlowService.getNextQuestion();
    }
  }

  private onNextQuestion(command: Model.QuizFlowCommandProceedContract): void {
    this.currentQuestion = {
        component: QuestionSingleRightComponent,
        inputs: {
          questionId: command.questionId,
          questionTemplate: command.template
        }
      };
  }
}
