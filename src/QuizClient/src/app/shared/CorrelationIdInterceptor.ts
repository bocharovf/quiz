import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse,
} from '@angular/common/http';

import ErrorCodes from '../quiz/ErrorCodes';
import ApplicationError from './errors/ApplicationError';

/** Manages correlation identifier Http header in requests and responses. */
export class CorrelationIdInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const correlationId: string = this.generateCorrelationId();
    request = request.clone({
      setHeaders: {
        'X-Correlation-ID': correlationId
      }
    });

    return next.handle(request).catch(this.handleError);
  }

  private generateCorrelationId(): string {
    return 'qc-' + Math.random().toString(36).substr(2, 9);
  }

  private handleError(error: HttpErrorResponse) {
    const correlationId = error.headers.get('X-Correlation-ID');
    const applicationError = new ApplicationError(error.message, ErrorCodes.ApiRequestError);
    applicationError.correlationId = correlationId;
    return Observable.throw(applicationError);
  }
}
