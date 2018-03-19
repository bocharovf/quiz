import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SharedModule } from './shared/shared.module';
import { NavigationService } from './shared/navigation.service';
import { QuizTemplatePageComponent } from './screen/quiz-template-page/quiz-template-page.component';
import { QuizFlowPageComponent } from './quiz/quiz-flow-page/quiz-flow-page.component';
import { ScorePageComponent } from './score/score-page/score-page.component';
import { NotFoundPageComponent } from './shared/not-found-page/not-found-page.component';
import { AuthRegisterPageComponent } from './auth/auth-register-page/auth-register-page.component';
import { AuthLoginPageComponent } from './auth/auth-login-page/auth-login-page.component';

/** Routing settings */
const routes: Routes = [
  { path: '', redirectTo: NavigationService.templatesRoute, pathMatch: 'full' },
  { path: NavigationService.templatesRoute, component: QuizTemplatePageComponent },
  { path: NavigationService.quizRoute, component: QuizFlowPageComponent },
  { path: NavigationService.scoresRoute, component: ScorePageComponent },
  { path: NavigationService.registrationRoute, component: AuthRegisterPageComponent },
  { path: NavigationService.loginRoute, component: AuthLoginPageComponent },
  { path: '**', component: NotFoundPageComponent }
];

/**
 * The routing module.
 */
@NgModule({
  imports: [ RouterModule.forRoot(routes), SharedModule ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
