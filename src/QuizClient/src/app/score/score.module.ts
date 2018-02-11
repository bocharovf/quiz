import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material';
import { RouterModule } from '@angular/router';

import { ScorePageComponent } from './score-page/score-page.component';
import { ScoreDataService } from './score-data.service';

/**
 * Scores area module.
 */
@NgModule({
  imports: [
    CommonModule,
    MatButtonModule,
    RouterModule
  ],
  declarations: [ScorePageComponent],
  providers: [ScoreDataService]
})
export class ScoreModule { }
