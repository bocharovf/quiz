import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { QuizScreenDataService } from './quiz-screen-data.service';
import { QuizTemplate, Quiz } from '../codegen/model.g';

describe('QuizScreenDataServiceService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      providers: [
        QuizScreenDataService
      ]
    });
  });

  afterEach(inject([HttpTestingController], (httpMock: HttpTestingController) => {
    httpMock.verify();
  }));

  describe('getQuizTemplates', () => {
    it('should make GET request to /api/quiz-templates',
      inject([QuizScreenDataService, HttpTestingController],
      (service: QuizScreenDataService, httpMock: HttpTestingController) => {
      const quizTemplates = new Array<QuizTemplate>();

      service.getQuizTemplates().subscribe(data => expect(data).toEqual(quizTemplates));

      const req = httpMock.expectOne((request) => request.url.indexOf('/api/quiz-templates') !== -1);
      expect(req.request.method).toEqual('GET');
      req.flush(quizTemplates);
    }));
  });

  describe('getQuizTemplate', () => {
    it('should make GET request to /api/quiz-templates/:id',
      inject([QuizScreenDataService, HttpTestingController],
      (service: QuizScreenDataService, httpMock: HttpTestingController) => {
      const quizTemplate = new QuizTemplate();

      service.getQuizTemplate(1).subscribe(data => expect(data).toEqual(quizTemplate));

      const req = httpMock.expectOne((request) => request.url.indexOf('/api/quiz-templates/1') !== -1);
      expect(req.request.method).toEqual('GET');
      req.flush(quizTemplate);
    }));
  });

  describe('getQuizzes', () => {
    it('should make GET request to /api/quizzes',
      inject([QuizScreenDataService, HttpTestingController],
      (service: QuizScreenDataService, httpMock: HttpTestingController) => {
      const quizzes = new Array<Quiz>();

      service.getQuizzes().subscribe(data => expect(data).toEqual(quizzes));

      const req = httpMock.expectOne((request) => request.url.indexOf('/api/quizzes') !== -1);
      expect(req.request.method).toEqual('GET');
      req.flush(quizzes);
    }));
  });

  describe('getQuiz', () => {
    it('should make GET request to /api/quizzes/:id',
      inject([QuizScreenDataService, HttpTestingController],
      (service: QuizScreenDataService, httpMock: HttpTestingController) => {
      const quiz = new Quiz();

      service.getQuiz(1).subscribe(data => expect(data).toEqual(quiz));

      const req = httpMock.expectOne((request) => request.url.indexOf('/api/quizzes/1') !== -1);
      expect(req.request.method).toEqual('GET');
      req.flush(quiz);
    }));
  });

});
