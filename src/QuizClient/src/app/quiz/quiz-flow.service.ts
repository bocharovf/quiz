import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { Observable, ObservableInput } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import { ReplaySubject } from 'rxjs/ReplaySubject';
import { tap, flatMap } from 'rxjs/operators';
import 'rxjs/add/observable/empty';
import 'rxjs/add/observable/throw';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/first';

import * as Model from '../codegen/model.g';
import { QuizFlowDataService } from './quiz-flow-data.service';
import { NavigationService } from '../shared/navigation.service';
import { ErrorHandlerService } from '../shared/errors/error-handler.service';
import ApplicationError from '../shared/errors/ApplicationError';
import ErrorCodes from './ErrorCodes';

@Injectable()
export class QuizFlowService {

  readonly activeQuiz$: Observable<Model.Quiz>;
  readonly quizCommand$: Observable<Model.QuizFlowCommandProceedContract>;
  readonly error$: Observable<any>;

  private activeQuizId = 0;

  private activeQuiz = new ReplaySubject<Model.Quiz>(1);
  private quizCommand = new Subject<Model.QuizFlowCommandProceedContract>();
  private error = new Subject<any>();

  constructor(
      private dataService: QuizFlowDataService,
      private navigation: NavigationService,
      private errorHandler: ErrorHandlerService) {
    this.activeQuiz$ = this.activeQuiz.asObservable();
    this.quizCommand$ = this.quizCommand.asObservable();
    this.error$ = this.error.asObservable();

    this.error$.subscribe((error) => this.errorHandler.handleError(error));
  }

  activateQuiz(quizId: number) {
    if (this.activeQuizId !== quizId) {
      this.dataService
          .getQuiz(quizId)
          .subscribe(quiz => {
            this.activeQuizId = quizId;
            this.activeQuiz.next(quiz);
          },
          error => this.handleError(error)
        );
    }
  }

  startNewQuiz(quizTemplate: Model.QuizTemplate) {
    this.dataService
        .startNewQuiz(quizTemplate.id)
        .subscribe(quiz => {
            this.activeQuizId = quiz.id;
            this.activeQuiz.next(quiz);
            this.navigation.goToQuiz(quiz.id);
          },
          error => this.handleError(error)
        );
  }

  getNextQuestion() {
    this.ensureActiveQuiz().pipe(
      flatMap(() => this.activeQuiz$.first()),
      flatMap(
        quiz => this.dataService.getNextQuestion(quiz.id),
        (quiz, command) => ({ quiz, command })
      )
    ).subscribe(
      data => this.dispatchCommand(data.command, data.quiz),
      error => this.handleError(error)
    );
  }

  answerQuestion(questionId: number, answer: Model.Answer) {
    this.ensureActiveQuiz().pipe(
      flatMap(() => this.activeQuiz$.first()),
      flatMap(quiz => this.dataService.answerQuestion(
                                        quiz.id,
                                        questionId,
                                        answer.templateId)
      )
    ).subscribe(
      () => this.getNextQuestion(),
      error => this.handleError(error)
    );
  }

  private dispatchCommand(command: Model.QuizFlowCommandContract, quiz: Model.Quiz) {
    if (command.commandType === Model.QuizFlowCommandType.QuizProceed) {
      const proceedCommand = command as Model.QuizFlowCommandProceedContract;
      this.quizCommand.next(proceedCommand);
    } else if (command.commandType === Model.QuizFlowCommandType.QuizFinish) {
      this.dataService
          .completeQuiz(quiz.id)
          .subscribe(() => this.navigation.goToQuizScores(quiz.id));
    }
  }

  private ensureActiveQuiz(): Observable<any> {
    if (this.activeQuizId === 0) {
      const error = new ApplicationError('Quiz is not active.', ErrorCodes.QuizIsNotActive);
      return Observable.throw(error);
    }

    return Observable.of(true);
  }

  private handleError(error: any) {
    const applicationError = new ApplicationError(error.message);
    applicationError.internalException = error;
    this.error.next(error);
  }
}
