import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material';

import { RegistrationContract, OperationResultContract, LocalizableErrorContract } from '../../codegen/model.g';
import { NavigationService } from '../../shared/navigation.service';
import { AuthService } from '../auth.service';

/**
 * Displays registration form.
 */
@Component({
  selector: 'quiz-auth-register-page',
  templateUrl: './auth-register-page.component.html',
  styleUrls: ['./auth-register-page.component.scss']
})
export class AuthRegisterPageComponent implements OnInit {
  private readonly SUCCESSFUL_REGISTRATION_MESSAGE = 'Registration succeeded';

  @ViewChild('registerForm') registerForm: NgForm;

  model = new RegistrationContract();
  errors = new Array<LocalizableErrorContract>();
  formValid = false;

  constructor(
    private auth: AuthService,
    private navigation: NavigationService,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit() {
    this.auth.registrationResult$.subscribe(
      status => this.handleRegisterResult(status)
    );

    this.registerForm.statusChanges
        .subscribe(() => {
          this.formValid = this.registerForm.form.valid;
        });
  }

  /** Register user. */
  register() {
    if (this.formValid) {
      this.auth.register(this.model);
    }
  }

  private handleRegisterResult(result: OperationResultContract) {
    this.errors = result.errors;

    if (result.isSuccessful) {
      this.snackBar.open(this.SUCCESSFUL_REGISTRATION_MESSAGE, null, {
        duration: 3000
      });

      this.navigation.goToHome();
    }
  }
}
