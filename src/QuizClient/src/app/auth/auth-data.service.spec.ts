import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { AuthDataService } from './auth-data.service';
import { RegistrationContract, OperationResultContract, LoginContract, AuthenticationStatusContract } from '../codegen/model.g';

describe('AuthDataService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      providers: [
        AuthDataService
      ]
    });
  });

  afterEach(inject([HttpTestingController], (httpMock: HttpTestingController) => {
    httpMock.verify();
  }));

  describe('register', () => {
    it('should make POST request to /api/auth/register with RegistrationContract payload',
      inject([AuthDataService, HttpTestingController],
      (service: AuthDataService, httpMock: HttpTestingController) => {
      const result = new OperationResultContract();
      const contract = new RegistrationContract();
      contract.email = 'test@domain.com';

      service.register(contract).subscribe(data => expect(data).toEqual(result));

      const req = httpMock.expectOne((request) => request.url.indexOf('/api/auth/register') !== -1);
      expect(req.request.method).toEqual('POST');
      expect(req.request.body).toEqual(contract);
      req.flush(result);
    }));
  });

  describe('login', () => {
    it('should make POST request to /api/auth/login with LoginContract payload',
      inject([AuthDataService, HttpTestingController],
      (service: AuthDataService, httpMock: HttpTestingController) => {
      const result = new OperationResultContract();
      const contract = new LoginContract();
      contract.login = 'test';

      service.login(contract).subscribe(data => expect(data).toEqual(result));

      const req = httpMock.expectOne((request) => request.url.indexOf('/api/auth/login') !== -1);
      expect(req.request.method).toEqual('POST');
      expect(req.request.body).toEqual(contract);
      req.flush(result);
    }));
  });

  describe('logout', () => {
    it('should make POST request to /api/auth/logout',
      inject([AuthDataService, HttpTestingController],
      (service: AuthDataService, httpMock: HttpTestingController) => {

      service.logout().subscribe();

      const req = httpMock.expectOne((request) => request.url.indexOf('/api/auth/logout') !== -1);
      expect(req.request.method).toEqual('POST');
      req.flush({});
    }));
  });

  describe('getStatus', () => {
    it('should make GET request to /api/auth/status',
      inject([AuthDataService, HttpTestingController],
      (service: AuthDataService, httpMock: HttpTestingController) => {
      const result = new AuthenticationStatusContract();

      service.getStatus().subscribe(data => expect(data).toEqual(result));

      const req = httpMock.expectOne((request) => request.url.indexOf('/api/auth/status') !== -1);
      expect(req.request.method).toEqual('GET');
      req.flush(result);
    }));
  });
});
