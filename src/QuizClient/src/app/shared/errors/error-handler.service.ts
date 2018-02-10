import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material';

import { IServiceExceptionContract } from '../../codegen/model.g';
import ApplicationError from './ApplicationError';

/**
 * Provides methods for handling exceptions.
 */
@Injectable()
export class ErrorHandlerService {
  private static readonly DEFAULT_ERROR_MESSAGE = 'Oops! Something went wrong ;(';
  private static readonly ERROR_ACTION_TEXT = 'Close';
  private static readonly SNACKBAR_DURATION = 3000;

  constructor(private snackBar: MatSnackBar) { }

  /**
   * Handles error.
   * @param error Error to handle.
   */
  handleError(error: any) {
    let errorMessage: string;

    if (this.isServiceError(error)) {
      // TODO: analyse errorCode and display localized message.
      errorMessage = error.message;
    }

    errorMessage = errorMessage || ErrorHandlerService.DEFAULT_ERROR_MESSAGE;
    this.logError(error);
    this.displayError(errorMessage);
  }

  private displayError(message: string) {
    this.snackBar.open(message, ErrorHandlerService.ERROR_ACTION_TEXT, {
      duration: ErrorHandlerService.SNACKBAR_DURATION
    });
  }

  private logError(error: any) {
    // TODO: log error on server.
    console.log('ERROR', error);
  }

  private isServiceError(error: any): error is IServiceExceptionContract {
    const serviceError = error as IServiceExceptionContract;
    return serviceError.errorCode !== undefined &&
           serviceError.message !== undefined;
  }
}
