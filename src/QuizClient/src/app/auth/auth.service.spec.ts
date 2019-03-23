import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Observable } from 'rxjs/Observable';
import { HttpErrorResponse } from '@angular/common/http';

import { AuthService } from './auth.service';
import { AuthDataService } from './auth-data.service';
import { OperationResultContract, RegistrationContract, AuthenticationStatusContract, User, LoginContract } from '../codegen/model.g';
import ApplicationError from '../shared/errors/ApplicationError';

describe('AuthService', () => {
  let dataService: any;
  let service: AuthService;

  beforeEach(() => {
    dataService = { };
    service = new AuthService(dataService);
  });

  describe('register', () => {
    it('should register new user', () => {
      const result = new OperationResultContract();
      const status = new AuthenticationStatusContract();
      const contract = new RegistrationContract();

      dataService.register = jasmine.createSpy('register')
                              .and.returnValue(Observable.of(result));
      dataService.getStatus = jasmine.createSpy('getStatus')
                              .and.returnValue(Observable.of(status));

      service.register(contract);

      expect(dataService.register).toHaveBeenCalledWith(contract);
    });

    it('should check authentication status', () => {
      const result = new OperationResultContract();
      const status = new AuthenticationStatusContract();
      const contract = new RegistrationContract();

      dataService.register = jasmine.createSpy('register')
                              .and.returnValue(Observable.of(result));
      dataService.getStatus = jasmine.createSpy('getStatus')
                              .and.returnValue(Observable.of(status));

      service.register(contract);

      expect(dataService.getStatus).toHaveBeenCalled();
    });

    it('should emit registration result if registration successful', (done) => {
      const result = new OperationResultContract();
      const status = new AuthenticationStatusContract();
      const contract = new RegistrationContract();

      dataService.register = jasmine.createSpy('register')
                              .and.returnValue(Observable.of(result));
      dataService.getStatus = jasmine.createSpy('getStatus')
                              .and.returnValue(Observable.of(status));
      service.registrationResult$
        .subscribe(value => {
          expect(value).toEqual(result);
          done();
        });

      service.register(contract);
    });

    it('should emit registration result if registration failed', (done) => {
      const result = new OperationResultContract();
      const registrationError = new ApplicationError('Error', 'ERR_CODE');
      registrationError.source = new HttpErrorResponse({
        error: result
      });

      const status = new AuthenticationStatusContract();
      const contract = new RegistrationContract();

      dataService.register = jasmine.createSpy('register')
                              .and.returnValue(Observable.throwError(registrationError));
      dataService.getStatus = jasmine.createSpy('getStatus')
                              .and.returnValue(Observable.of(status));
      service.registrationResult$
        .subscribe(value => {
          expect(value).toEqual(result);
          done();
        });

      service.register(contract);
    });

    it('should emit User if registration successful', (done) => {
      const result = new OperationResultContract();
      const user = new User();
      const status = new AuthenticationStatusContract();
      status.user = user;
      const contract = new RegistrationContract();

      dataService.register = jasmine.createSpy('register')
                              .and.returnValue(Observable.of(result));
      dataService.getStatus = jasmine.createSpy('getStatus')
                              .and.returnValue(Observable.of(status));
      service.user$
        .subscribe(value => {
          expect(value).toEqual(user);
          done();
        });

      service.register(contract);
    });
  });

  describe('login', () => {
    it('should login a user', () => {
      const result = new OperationResultContract();
      const status = new AuthenticationStatusContract();
      const contract = new LoginContract();

      dataService.login = jasmine.createSpy('login')
                              .and.returnValue(Observable.of(result));
      dataService.getStatus = jasmine.createSpy('getStatus')
                              .and.returnValue(Observable.of(status));

      service.login(contract);

      expect(dataService.login).toHaveBeenCalledWith(contract);
    });

    it('should check authentication status', () => {
      const result = new OperationResultContract();
      const status = new AuthenticationStatusContract();
      const contract = new LoginContract();

      dataService.login = jasmine.createSpy('login')
                              .and.returnValue(Observable.of(result));
      dataService.getStatus = jasmine.createSpy('getStatus')
                              .and.returnValue(Observable.of(status));

      service.login(contract);

      expect(dataService.getStatus).toHaveBeenCalled();
    });

    it('should emit login result if login successful', (done) => {
      const result = new OperationResultContract();
      const status = new AuthenticationStatusContract();
      const contract = new LoginContract();

      dataService.login = jasmine.createSpy('login')
                              .and.returnValue(Observable.of(result));
      dataService.getStatus = jasmine.createSpy('getStatus')
                              .and.returnValue(Observable.of(status));
      service.loginResult$
        .subscribe(value => {
          expect(value).toEqual(result);
          done();
        });

      service.login(contract);
    });

    it('should emit login result if login failed', (done) => {
      const result = new OperationResultContract();
      const loginError = new ApplicationError('Error', 'ERR_CODE');
      loginError.source = new HttpErrorResponse({
        error: result
      });

      const status = new AuthenticationStatusContract();
      const contract = new LoginContract();

      dataService.login = jasmine.createSpy('login')
                              .and.returnValue(Observable.throwError(loginError));
      dataService.getStatus = jasmine.createSpy('getStatus')
                              .and.returnValue(Observable.of(status));
      service.loginResult$
        .subscribe(value => {
          expect(value).toEqual(result);
          done();
        });

      service.login(contract);
    });

    it('should emit User if registration successful', (done) => {
      const result = new OperationResultContract();
      const user = new User();
      const status = new AuthenticationStatusContract();
      status.user = user;
      const contract = new LoginContract();

      dataService.login = jasmine.createSpy('login')
                              .and.returnValue(Observable.of(result));
      dataService.getStatus = jasmine.createSpy('getStatus')
                              .and.returnValue(Observable.of(status));
      service.user$
        .subscribe(value => {
          expect(value).toEqual(user);
          done();
        });

      service.login(contract);
    });
  });

  describe('logout', () => {
    it('should logout a user', () => {
      dataService.logout = jasmine.createSpy('logout')
                            .and.returnValue(Observable.of({}));

      service.logout();

      expect(dataService.logout).toHaveBeenCalled();
    });

    it('should emit empty (null) User', (done) => {
      dataService.logout = jasmine.createSpy('logout')
                            .and.returnValue(Observable.of({}));

      service.user$
        .subscribe(value => {
          expect(value).toBeNull();
          done();
        });

      service.logout();
    });
  });
});
