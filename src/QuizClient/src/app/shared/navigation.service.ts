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

  /** Registration page route. */
  static readonly registrationRoute = 'registration';

  /** Login page route. */
  static readonly loginRoute = 'login';

  /** Error 404 page route. */
  static readonly error404Route = 'error/404';

  /** Default route. */
  static readonly homeRoute = NavigationService.templatesRoute;

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

  /**
   * Navigates to registration page.
   */
  goToRegistration(): void {
    this.router.navigate([NavigationService.registrationRoute]);
  }

  /**
   * Navigates to login page.
   */
  goToLogin(): void {
    this.router.navigate([NavigationService.loginRoute]);
  }

  /**
   * Navigates to home page.
   */
  goToHome(): void {
    this.router.navigate([NavigationService.homeRoute]);
  }

  /**
   * Navigates to not found page.
   */
  goToNotFound() {
    this.router.navigate([NavigationService.error404Route]);
  }
}
