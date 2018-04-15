import { Component, OnInit } from '@angular/core';

import { NavigationService } from '../navigation.service';
import { AuthService } from '../../auth/auth.service';
import { User } from '../../codegen/model.g';

/**
 * Application toolbar component.
 */
@Component({
  selector: 'quiz-toolbar',
  templateUrl: './quiz-toolbar.component.html',
  styleUrls: ['./quiz-toolbar.component.scss']
})
export class QuizToolbarComponent implements OnInit {
  user: User;

  homeLink: string;
  registrationLink: string;
  loginLink: string;

  isAuthenticated: boolean;

  constructor(private navigation: NavigationService, private auth: AuthService) {
    this.homeLink = '/' + NavigationService.homeRoute;
    this.registrationLink = '/' + NavigationService.registrationRoute;
    this.loginLink = '/' + NavigationService.loginRoute;
  }

  ngOnInit() {
    this.auth.user$.subscribe(user => {
      this.isAuthenticated = !!user;
      this.user = user;
    });
  }

  /** Performs logout operation. */
  logout() {
    this.auth.logout();
    this.navigation.goToHome();
  }
}
