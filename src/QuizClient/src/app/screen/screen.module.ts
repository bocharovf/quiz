import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatListModule, MatSidenavModule, MatButtonModule } from '@angular/material';

import { QuizScreenDataService } from './quiz-screen-data.service';
import { QuizTemplatePageComponent } from './quiz-template-page/quiz-template-page.component';
import { QuizTemplateListComponent } from './quiz-template-list/quiz-template-list.component';
import { QuizTemplateDetailsComponent } from './quiz-template-details/quiz-template-details.component';

@NgModule({
  imports: [
    CommonModule,
    MatListModule, MatSidenavModule, MatButtonModule
  ],
  providers: [ QuizScreenDataService ],
  exports: [ QuizTemplatePageComponent ],
  declarations: [
    QuizTemplatePageComponent,
    QuizTemplateListComponent,
    QuizTemplateDetailsComponent
  ]
})
export class ScreenModule { }
