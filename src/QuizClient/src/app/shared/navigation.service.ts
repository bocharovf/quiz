import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Routes } from '@angular/router';

/**
 * Provides methods to perform application navigation.
 */
@Injectable()
export class NavigationService {

  constructor(private router: Router) { }

  /** Quiz template page route. */
  static readonly templatesRoute = 'quizzes/templates';

  /** Quiz page route. */
  static readonly quizRoute = 'quizzes/:id';

  /** Quiz scores page route. */
  static readonly scoresRoute = 'quizzes/:id/scores';

  /** Default route link. */
  static readonly homeLink = '/' + NavigationService.templatesRoute;

  /**
   * Navigates to quiz flow page.
   * @param quizId Quiz identifier.
   */
  goToQuiz(quizId: number) {
    const route = NavigationService.quizRoute.replace(':id', String(quizId));
    this.router.navigate([route]);
  }

  /**
   * Navigates to quiz score page.
   * @param quizId Quiz identifier.
   */
  goToQuizScores(quizId: number): void {
    const route = NavigationService.scoresRoute.replace(':id', String(quizId));
    this.router.navigate([route]);
  }
}
