import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatToolbarModule, MatIconModule, MatSnackBarModule } from '@angular/material';

import { NavigationService } from './navigation.service';
import { ErrorHandlerService } from './errors/error-handler.service';
import { QuizToolbarComponent } from './quiz-toolbar/quiz-toolbar.component';
import { NotFoundPageComponent } from './not-found-page/not-found-page.component';

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
  providers: [NavigationService, ErrorHandlerService]
})
export class SharedModule { }
