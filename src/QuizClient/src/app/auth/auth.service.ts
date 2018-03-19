import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { ReplaySubject } from 'rxjs/ReplaySubject';
import { Subject } from 'rxjs/Subject';
import { tap, flatMap, catchError } from 'rxjs/operators';

import {
  RegistrationContract, LoginContract, User,
  AuthenticationStatusContract, OperationResultContract
} from '../codegen/model.g';
import { AuthDataService } from './auth-data.service';
import ApplicationError from '../shared/errors/ApplicationError';

/**
 * Provides authentication methods.
 */
@Injectable()
export class AuthService {

  /** Current user stream. */
  readonly user$: Observable<User>;

  /** Login result stream. */
  readonly loginResult$: Observable<OperationResultContract>;

  /** Registration result stream. */
  readonly registrationResult$: Observable<OperationResultContract>;

  private user = new ReplaySubject<User>(1);
  private loginResult = new Subject<OperationResultContract>();
  private registrationResult = new Subject<OperationResultContract>();

  constructor(private authData: AuthDataService) {
    this.user$ = this.user.asObservable();
    this.loginResult$ = this.loginResult.asObservable();
    this.registrationResult$ = this.registrationResult.asObservable();
  }

  /** Updates user authentication status. */
  updateStatus() {
    this.authData.getStatus()
        .subscribe(
          status => this.user.next(status.user)
        );
  }

  /**
   * Registers new user.
   * @param registrationContract registration information.
   */
  register(registrationContract: RegistrationContract) {
    this.authData.register(registrationContract)
      .pipe(
        tap(result => this.registrationResult.next(result)),
        catchError((err: ApplicationError, src) => {
          const response = err.source as HttpErrorResponse;
          const result = response.error as OperationResultContract;
          this.registrationResult.next(result);
          return Observable.of(result);
        }),
        flatMap(() => this.authData.getStatus())
      ).subscribe(status => {
        this.user.next(status.user);
      });
  }

  /**
   * Login user.
   * @param loginContract login information.
   */
  login(loginContract: LoginContract) {
    this.authData.login(loginContract)
      .pipe(
        tap(result => this.loginResult.next(result)),
        catchError((err: ApplicationError, src) => {
          const response = err.source as HttpErrorResponse;
          const result = response.error as OperationResultContract;
          this.loginResult.next(result);
          return Observable.of(result);
        }),
        flatMap(() => this.authData.getStatus())
      ).subscribe(status => {
        this.user.next(status.user);
      });
  }

  /**
   * Logout user.
   */
  logout() {
    this.authData.logout()
      .subscribe(() => this.user.next(null));
  }
}
