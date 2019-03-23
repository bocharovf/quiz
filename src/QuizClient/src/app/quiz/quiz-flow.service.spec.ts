import { TestBed, inject } from '@angular/core/testing';
import { Observable } from 'rxjs/Observable';

import { QuizFlowService } from './quiz-flow.service';
import { QuizFlowDataService } from './quiz-flow-data.service';
import { NavigationService } from '../shared/navigation.service';
import { ErrorHandlerService } from '../shared/errors/error-handler.service';
import ApplicationError from '../shared/errors/ApplicationError';
import ErrorCodes from './ErrorCodes';
import * as Model from '../codegen/model.g';

describe('QuizFlowService', () => {
  let dataService: any;
  let navigation: any;
  let errorHandler: any;

  let service: QuizFlowService;

  beforeEach(() => {
    dataService = { };
    navigation = jasmine.createSpyObj(['goToQuiz', 'goToQuizScores']);
    errorHandler = jasmine.createSpyObj(['handleError']);
    service = new QuizFlowService(dataService, navigation, errorHandler);
  });

  describe('activateQuiz', () => {
    it('should load specified quiz by id', () => {
      const quiz = new Model.Quiz();
      dataService.getQuiz = jasmine.createSpy('getQuiz')
                              .and.returnValue(Observable.of(quiz));

      service.activateQuiz(42);

      expect(dataService.getQuiz).toHaveBeenCalledWith(42);
    });

    it('should emit loaded quiz to active quiz stream', (done) => {
      const quiz = new Model.Quiz();
      dataService.getQuiz = jasmine.createSpy('getQuiz')
                              .and.returnValue(Observable.of(quiz));

      service.activeQuiz$.subscribe(activeQuiz => {
        expect(activeQuiz).toEqual(quiz);
        done();
      });

      service.activateQuiz(42);
    });

    it('should do nothing if the same quiz already activated', () => {
      const quiz = new Model.Quiz();
      quiz.id = 42;

      dataService.getQuiz = jasmine.createSpy('getQuiz')
                              .and.returnValue(Observable.of(quiz));

      service.activateQuiz(42);
      service.activateQuiz(42);
      expect(dataService.getQuiz).toHaveBeenCalledTimes(1);
    });

    it('should pass errors to error handling service', () => {
      const error = new Error('error');
      dataService.getQuiz = jasmine.createSpy('getQuiz')
                              .and.returnValue(Observable.throwError(error));

      service.activateQuiz(42);

      expect(errorHandler.handleError).toHaveBeenCalledWith(error);
    });
  });

  describe('startNewQuiz', () => {
    it('should call API to start new quiz', () => {
      const quiz = new Model.Quiz();
      const quizTemplate = new Model.QuizTemplate();
      quizTemplate.id = 43;
      dataService.startNewQuiz = jasmine.createSpy('startNewQuiz')
                                  .and.returnValue(Observable.of(quiz));

      service.startNewQuiz(quizTemplate);

      expect(dataService.startNewQuiz).toHaveBeenCalledWith(43);
    });

    it('should navigate to quiz page', () => {
      const quiz = new Model.Quiz();
      quiz.id = 33;
      const quizTemplate = new Model.QuizTemplate();
      dataService.startNewQuiz = jasmine.createSpy('startNewQuiz')
                                  .and.returnValue(Observable.of(quiz));

      service.startNewQuiz(quizTemplate);

      expect(navigation.goToQuiz).toHaveBeenCalledWith(33);
    });

    it('should emit new quiz to active quiz stream', (done) => {
      const quiz = new Model.Quiz();
      const quizTemplate = new Model.QuizTemplate();
      dataService.startNewQuiz = jasmine.createSpy('startNewQuiz')
                                  .and.returnValue(Observable.of(quiz));

      service.activeQuiz$.subscribe(activeQuiz => {
        expect(activeQuiz).toEqual(quiz);
        done();
      });

      service.startNewQuiz(quizTemplate);
    });

    it('should pass errors to error handling service', () => {
      const error = new Error('error');
      const quizTemplate = new Model.QuizTemplate();
      dataService.startNewQuiz = jasmine.createSpy('startNewQuiz')
                                   .and.returnValue(Observable.throwError(error));

      service.startNewQuiz(quizTemplate);

      expect(errorHandler.handleError).toHaveBeenCalledWith(error);
    });
  });

  describe('getNextQuestion', () => {
    it('should fail with QuizIsNotActive error if quiz is not active', () => {
      service.getNextQuestion();

      const args = errorHandler.handleError.calls.argsFor(0);
      expect(args[0] instanceof ApplicationError).toBeTruthy();
      expect(args[0].errorCode).toEqual(ErrorCodes.QuizIsNotActive);
    });

    it('should call API to get next question', () => {
      const quiz = new Model.Quiz();
      quiz.id = 11;
      const command = new Model.QuizFlowCommandContract();
      dataService.getQuiz = jasmine.createSpy('getQuiz')
                                      .and.returnValue(Observable.of(quiz));
      dataService.getNextQuestion = jasmine.createSpy('getNextQuestion')
                                      .and.returnValue(Observable.of(command));
      service.activateQuiz(11);

      service.getNextQuestion();

      expect(dataService.getNextQuestion).toHaveBeenCalledWith(11);
    });

    it('should pass received proceed quiz command to quiz command flow', (done) => {
      const quiz = new Model.Quiz();
      quiz.id = 11;
      const proceedCommand = new Model.QuizFlowCommandProceedContract();
      proceedCommand.commandType = Model.QuizFlowCommandType.QuizProceed;
      dataService.getQuiz = jasmine.createSpy('getQuiz')
                                      .and.returnValue(Observable.of(quiz));
      dataService.getNextQuestion = jasmine.createSpy('getNextQuestion')
                                      .and.returnValue(Observable.of(proceedCommand));
      service.activateQuiz(11);
      service.quizCommand$.subscribe(command => {
        expect(command).toEqual(proceedCommand);
        done();
      });

      service.getNextQuestion();
    });

    it('should call API to complete quiz if finish command received', () => {
      const quiz = new Model.Quiz();
      quiz.id = 11;
      const finishCommand = new Model.QuizFlowCommandFinishContract();
      finishCommand.commandType = Model.QuizFlowCommandType.QuizFinish;
      dataService.getQuiz = jasmine.createSpy('getQuiz')
                                      .and.returnValue(Observable.of(quiz));
      dataService.getNextQuestion = jasmine.createSpy('getNextQuestion')
                                      .and.returnValue(Observable.of(finishCommand));
      dataService.completeQuiz = jasmine.createSpy('completeQuiz')
                                      .and.returnValue(Observable.of({}));
      service.activateQuiz(11);

      service.getNextQuestion();

      expect(dataService.completeQuiz).toHaveBeenCalledWith(11);
    });

    it('should pass errors to error handling service', () => {
      const error = new Error('error');
      const quiz = new Model.Quiz();
      quiz.id = 11;
      const finishCommand = new Model.QuizFlowCommandFinishContract();
      finishCommand.commandType = Model.QuizFlowCommandType.QuizFinish;
      dataService.getQuiz = jasmine.createSpy('getQuiz')
                                      .and.returnValue(Observable.of(quiz));
      dataService.getNextQuestion = jasmine.createSpy('getNextQuestion')
                                      .and.returnValue(Observable.throwError(error));
      dataService.completeQuiz = jasmine.createSpy('completeQuiz')
                                      .and.returnValue(Observable.of({}));
      service.activateQuiz(11);

      service.getNextQuestion();

      expect(errorHandler.handleError).toHaveBeenCalledWith(error);
    });
  });

  describe('answerQuestion', () => {
    it('should fail with QuizIsNotActive error if quiz is not active', () => {
      const answer = new Model.Answer();

      service.answerQuestion(10, answer);

      const args = errorHandler.handleError.calls.argsFor(0);
      expect(args[0] instanceof ApplicationError).toBeTruthy();
      expect(args[0].errorCode).toEqual(ErrorCodes.QuizIsNotActive);
    });

    it('should call API to answer question', () => {
      const answer = new Model.Answer();
      answer.templateId = 55;
      const quiz = new Model.Quiz();
      quiz.id = 11;
      const command = new Model.QuizFlowCommandContract();
      dataService.getQuiz = jasmine.createSpy('getQuiz')
                                      .and.returnValue(Observable.of(quiz));
      dataService.getNextQuestion = jasmine.createSpy('getNextQuestion')
                                      .and.returnValue(Observable.throwError(command));
      dataService.answerQuestion = jasmine.createSpy('answerQuestion')
                                      .and.returnValue(Observable.of({}));
      service.activateQuiz(11);

      service.answerQuestion(15, answer);

      expect(dataService.answerQuestion).toHaveBeenCalledWith(11, 15, 55);
    });

    it('should call getNextQuestion service method', () => {
      const answer = new Model.Answer();
      answer.templateId = 55;
      const quiz = new Model.Quiz();
      quiz.id = 11;
      const command = new Model.QuizFlowCommandContract();
      dataService.getQuiz = jasmine.createSpy('getQuiz')
                                      .and.returnValue(Observable.of(quiz));
      dataService.getNextQuestion = jasmine.createSpy('getNextQuestion')
                                      .and.returnValue(Observable.throwError(command));
      dataService.answerQuestion = jasmine.createSpy('answerQuestion')
                                      .and.returnValue(Observable.of({}));
      service.activateQuiz(11);
      service.getNextQuestion = jasmine.createSpy('getNextQuestion');

      service.answerQuestion(15, answer);

      expect(service.getNextQuestion).toHaveBeenCalled();
    });

    it('should pass errors to error handling service', () => {
      const error = new Error('error');
      const answer = new Model.Answer();
      answer.templateId = 55;
      const quiz = new Model.Quiz();
      quiz.id = 11;
      dataService.getQuiz = jasmine.createSpy('getQuiz')
                                      .and.returnValue(Observable.of(quiz));
      dataService.getNextQuestion = jasmine.createSpy('getNextQuestion')
                                      .and.returnValue(Observable.throwError(error));
      dataService.answerQuestion = jasmine.createSpy('answerQuestion')
                                      .and.returnValue(Observable.throwError(error));
      service.activateQuiz(11);

      service.answerQuestion(15, answer);

      expect(errorHandler.handleError).toHaveBeenCalledWith(error);
    });
  });
});
