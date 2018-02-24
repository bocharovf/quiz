import { TestBed, inject } from '@angular/core/testing';
import { HttpXhrBackend } from '@angular/common/http';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { HttpTestingController } from '@angular/common/http/testing';

import { LoggingDataService } from './logging-data.service';
import ApplicationError from './errors/ApplicationError';

describe('LoggingDataService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      providers: [
        LoggingDataService
      ]
    });
  });

  afterEach(inject([HttpTestingController], (httpMock: HttpTestingController) => {
    httpMock.verify();
  }));

  describe('logError', () => {
    it('should make POST request to /api/log/error with template id',
      inject([LoggingDataService, HttpTestingController],
      (service: LoggingDataService, httpMock: HttpTestingController) => {

      const error = new ApplicationError('Error');
      service.logError(error);

      const req = httpMock.expectOne((request) => request.url.indexOf('/api/log/error') !== -1);
      expect(req.request.method).toEqual('POST');
      expect(req.request.body).toBeTruthy();
      req.flush(error);
    }));
  });
});
