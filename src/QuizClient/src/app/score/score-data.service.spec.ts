import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { ScoreDataService } from './score-data.service';
import { Score } from '../codegen/model.g';

describe('ScoreDataService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      providers: [
        ScoreDataService
      ]
    });
  });

  afterEach(inject([HttpTestingController], (httpMock: HttpTestingController) => {
    httpMock.verify();
  }));

  describe('getQuizScores', () => {
    it('should make GET request to /api/quizzes/:id/scores',
      inject([ScoreDataService, HttpTestingController],
      (service: ScoreDataService, httpMock: HttpTestingController) => {
      const score = new Score();

      service.getQuizScores(1).subscribe(data => expect(data).toEqual(score));

      const req = httpMock.expectOne((request) => request.url.indexOf('/api/quizzes/1/scores') !== -1);
      expect(req.request.method).toEqual('GET');
      req.flush(score);
    }));
  });

});
