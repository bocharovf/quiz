import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatToolbarModule, MatIconModule, MatSnackBarModule } from '@angular/material';

import { NavigationService } from './navigation.service';
import { ErrorHandlerService } from './errors/error-handler.service';
import { QuizToolbarComponent } from './quiz-toolbar/quiz-toolbar.component';
import { NotFoundPageComponent } from './not-found-page/not-found-page.component';
import { CorrelationIdInterceptor } from './CorrelationIdInterceptor';
import { LoggingDataService } from './logging-data.service';

/**
 * Provides shared functionality.
 */
@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    MatToolbarModule,
    MatIconModule,
    MatSnackBarModule
  ],
  exports: [
    QuizToolbarComponent
  ],
  declarations: [QuizToolbarComponent, NotFoundPageComponent],
  providers: [
    ErrorHandlerService,
    {
      provide: ErrorHandler,
      useClass: ErrorHandlerService
    },
    LoggingDataService,
    NavigationService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: CorrelationIdInterceptor,
      multi: true
    }]
})
export class SharedModule { }
