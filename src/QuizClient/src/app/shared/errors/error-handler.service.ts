import { Injectable, ErrorHandler, Injector } from '@angular/core';
import { MatSnackBar } from '@angular/material';

import ApplicationError from './ApplicationError';
import { LoggingDataService } from '../logging-data.service';

/**
 * Provides methods for handling exceptions.
 */
@Injectable()
export class ErrorHandlerService implements ErrorHandler {
  private static readonly DEFAULT_ERROR_MESSAGE = 'Oops! Something went wrong ;(';
  private static readonly ERROR_ACTION_TEXT = 'Close';
  private static readonly SNACKBAR_DURATION = 3000;

  constructor(private injector: Injector) { }

  /**
   * Handles error.
   * @param error Error to handle.
   */
  handleError(error: any) {
    const dataService = this.injector.get(LoggingDataService);
    const snackBar = this.injector.get(MatSnackBar);

    let applicationError: ApplicationError;
    let displayMessage: string;

    if (error instanceof ApplicationError) {
      applicationError = error;
      displayMessage = error.message;
    } else {
      applicationError = new ApplicationError(error.message);
      displayMessage = ErrorHandlerService.DEFAULT_ERROR_MESSAGE;
      applicationError.stackTrace = error.stack;
    }

    applicationError.clientPlatform = navigator.userAgent;

    this.logError(applicationError, dataService);
    this.displayError(displayMessage, snackBar);
  }

  private displayError(message: string, snackBar: MatSnackBar) {
    snackBar.open(message, ErrorHandlerService.ERROR_ACTION_TEXT, {
      duration: ErrorHandlerService.SNACKBAR_DURATION
    });
  }

  private logError(error: ApplicationError, dataService: LoggingDataService) {
    console.error('ERROR', error);
    dataService.logError(error);
  }
}
