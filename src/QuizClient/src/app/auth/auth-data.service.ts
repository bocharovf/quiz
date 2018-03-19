import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { BaseApiUrl } from '../shared/utils/DataUtils';
import { RegistrationContract, LoginContract, AuthenticationStatusContract, OperationResultContract } from '../codegen/model.g';

/**
 * Provides methods to authenticate user on server.
 */
@Injectable()
export class AuthDataService {
  private readonly baseUrl = `${BaseApiUrl}/auth`;

  constructor(private http: HttpClient) { }

  /**
   * Registers new user.
   * @param registrationContract Registration contract.
   */
  register(registrationContract: RegistrationContract): Observable<OperationResultContract> {
    const url = `${this.baseUrl}/register`;
    return this.http.post<OperationResultContract>(url, registrationContract);
  }

  /**
   * Performs login operation.
   * @param registrationContract Registration contract.
   */
  login(loginContract: LoginContract): Observable<OperationResultContract> {
    const url = `${this.baseUrl}/login`;
    return this.http.post<OperationResultContract>(url, loginContract);
  }

  /**
   * Performs logout operation.
   */
  logout(): Observable<{}> {
    const url = `${this.baseUrl}/logout`;
    return this.http.post<{}>(url, null);
  }

  /**
   * Gets authentication status.
   */
  getStatus(): Observable<AuthenticationStatusContract> {
    const url = `${this.baseUrl}/status`;
    return this.http.get<AuthenticationStatusContract>(url);
  }
}
