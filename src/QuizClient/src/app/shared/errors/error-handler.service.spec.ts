import { TestBed, inject } from '@angular/core/testing';

import { ErrorHandlerService } from './error-handler.service';
import ApplicationError from './ApplicationError';

describe('ErrorHandlerService', () => {
  let snackBar: any;
  let service: ErrorHandlerService;

  beforeEach(() => {
    snackBar = jasmine.createSpyObj(['open']);
    service = new ErrorHandlerService(snackBar);
  });

  describe('handleError', () => {
    it('should display custom error message in snack bar for application errors', () => {
      const error = new ApplicationError('Test error message', 'ERR_CODE');

      service.handleError(error);

      expect(snackBar.open).toHaveBeenCalled();
      const args = snackBar.open.calls.argsFor(0);
      expect(args[0]).toEqual('Test error message');
    });

    it('should display default error message in snack bar for unknown errors', () => {
      const error = 'unknown error';

      service.handleError(error);

      expect(snackBar.open).toHaveBeenCalled();
      const args = snackBar.open.calls.argsFor(0);
      expect(args[0]).toEqual('Oops! Something went wrong ;(');
    });
  });
});
