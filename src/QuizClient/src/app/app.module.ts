import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';
import { ScreenModule } from './screen/screen.module';
import { QuizModule } from './quiz/quiz.module';
import { ScoreModule } from './score/score.module';
import { AppComponent } from './app.component';

/**
 * The bootstrapper module.
 */
@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    NoopAnimationsModule,
    AppRoutingModule,
    HttpClientModule,

    SharedModule,
    ScreenModule,
    QuizModule,
    ScoreModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
