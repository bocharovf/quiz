import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Routes } from '@angular/router';

@Injectable()
export class NavigationService {

  constructor(private router: Router) { }

  static readonly templatesRoute = 'quizzes/templates';
  static readonly quizRoute = 'quizzes/:id';
  static readonly scoresRoute = 'quizzes/:id/scores';

  static readonly homeLink = '/' + NavigationService.templatesRoute;

  goToQuiz(quizId: number) {
    const route = NavigationService.quizRoute.replace(':id', String(quizId));
    this.router.navigate([route]);
  }

  goToQuizScores(quizId: number): void {
    const route = NavigationService.scoresRoute.replace(':id', String(quizId));
    this.router.navigate([route]);
  }
}
