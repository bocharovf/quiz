import { HttpRequest, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import 'rxjs/add/observable/of';

import { CorrelationIdInterceptor } from './CorrelationIdInterceptor';
import ApplicationError from './errors/ApplicationError';

describe('CorrelationIdInterceptor', () => {
  let interceptor: CorrelationIdInterceptor;

  beforeEach(() => {
    interceptor = new CorrelationIdInterceptor();
  });

  describe('intercept', () => {
    it('should add correlation id HTTP header to request', (done) => {
        const request = new HttpRequest('GET', 'localhost');
        const httpHandler = {
            handle(req: HttpRequest<any>): Observable<any> {
                const correlationId = req.headers.get('X-Correlation-ID');
                expect(correlationId).toMatch('qc-.*');
                return Observable.of({});
            }
        };

        interceptor.intercept(request, httpHandler).subscribe(() => {
            done();
        });
    });

    it('should convert HTTP exception to ApplicationException with correlation id', (done) => {
        const errorHeaders: HttpHeaders = new HttpHeaders(
            {
                'X-Correlation-ID': 'qc-12345'
            }
        );
        const errorResponse: HttpErrorResponse = new HttpErrorResponse({
            headers: errorHeaders
        });

        const request = new HttpRequest('GET', 'localhost');
        const httpHandler = {
            handle(req: HttpRequest<any>): Observable<any> {
                const correlationId = req.headers.get('X-Correlation-ID');
                return Observable.throw(errorResponse);
            }
        };

        interceptor.intercept(request, httpHandler).subscribe(
            () => { fail('observable should fail'); done(); },
            (error) => {
                expect(error instanceof ApplicationError).toBeTruthy('should be of type ApplicationError');
                expect(error.correlationId).toEqual('qc-12345');
                done();
            }
        );
    });
  });
});
