import { TestBed, inject } from '@angular/core/testing';
import { HttpXhrBackend } from '@angular/common/http';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { HttpTestingController } from '@angular/common/http/testing';

import { QuizTemplate, Quiz, QuizFlowCommandContract } from '../codegen/model.g';
import { QuizFlowDataService } from './quiz-flow-data.service';

describe('QuizFlowDataService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      providers: [
        QuizFlowDataService
      ]
    });
  });

  afterEach(inject([HttpTestingController], (httpMock: HttpTestingController) => {
    httpMock.verify();
  }));

  describe('startNewQuiz', () => {
    it('should make POST request to /api/quizzes with template id',
      inject([QuizFlowDataService, HttpTestingController],
      (service: QuizFlowDataService, httpMock: HttpTestingController) => {
      const quiz = new Quiz();
      quiz.id = 2;

      service.startNewQuiz(1).subscribe(data => expect(data).toEqual(quiz));

      const req = httpMock.expectOne((request) => request.url.indexOf('/api/quizzes') !== -1);
      expect(req.request.method).toEqual('POST');
      expect(req.request.params.get('templateId')).toEqual('1');
      req.flush(quiz);
    }));
  });

  describe('getNextQuestion', () => {
    it('should make POST request to /api/:id/nextquestion',
      inject([QuizFlowDataService, HttpTestingController],
      (service: QuizFlowDataService, httpMock: HttpTestingController) => {
      const contract = new QuizFlowCommandContract();

      service.getNextQuestion(1).subscribe(data => expect(data).toEqual(contract));

      const req = httpMock.expectOne((request) => request.url.indexOf('/api/quizzes/1/nextquestion') !== -1);
      expect(req.request.method).toEqual('POST');
      req.flush(contract);
    }));
  });

  describe('answerQuestion', () => {
    it('should make PATCH request to /api/:id/questions/:id with answer template id',
      inject([QuizFlowDataService, HttpTestingController],
      (service: QuizFlowDataService, httpMock: HttpTestingController) => {
      service.answerQuestion(1, 2, 3).subscribe();

      const req = httpMock.expectOne((request) => request.url.indexOf('/api/quizzes/1/questions/2') !== -1);
      expect(req.request.method).toEqual('PATCH');
      expect(req.request.params.get('answerTemplateId')).toEqual('3');
      req.flush({});
    }));
  });

  describe('completeQuiz', () => {
    it('should make POST request to /api/:id/complete',
      inject([QuizFlowDataService, HttpTestingController],
      (service: QuizFlowDataService, httpMock: HttpTestingController) => {
      service.completeQuiz(1).subscribe();

      const req = httpMock.expectOne((request) => request.url.indexOf('/api/quizzes/1/complete') !== -1);
      expect(req.request.method).toEqual('POST');
      req.flush({});
    }));
  });
});
