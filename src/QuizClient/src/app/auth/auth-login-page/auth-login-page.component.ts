import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material';

import { LoginContract, OperationResultContract, LocalizableErrorContract } from '../../codegen/model.g';
import { AuthService } from '../auth.service';
import { NavigationService } from '../../shared/navigation.service';

/**
 * Displays login form.
 */
@Component({
  selector: 'quiz-auth-login-page',
  templateUrl: './auth-login-page.component.html',
  styleUrls: ['./auth-login-page.component.scss']
})
export class AuthLoginPageComponent implements OnInit {
  private readonly SUCCESSFUL_login_MESSAGE = 'Login succeeded';

  @ViewChild('loginForm') loginForm: NgForm;

  model = new LoginContract();
  errors = new Array<LocalizableErrorContract>();
  formValid = false;

  constructor(
    private auth: AuthService,
    private navigation: NavigationService,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit() {
    this.auth.loginResult$.subscribe(
      status => this.handleLoginResult(status)
    );

    this.loginForm.statusChanges
        .subscribe(() => {
          this.formValid = this.loginForm.form.valid;
        });
  }

  /** Login user. */
  login() {
    if (this.formValid) {
      this.auth.login(this.model);
    }
  }

  private handleLoginResult(result: OperationResultContract) {
    this.errors = result.errors;

    if (result.isSuccessful) {
      this.snackBar.open(this.SUCCESSFUL_login_MESSAGE, null, {
        duration: 3000
      });

      this.navigation.goToHome();
    }
  }
}
