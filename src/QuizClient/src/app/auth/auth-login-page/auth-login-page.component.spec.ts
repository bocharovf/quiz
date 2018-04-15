import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { MatInputModule, MatButtonModule, MatCheckboxModule } from '@angular/material';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { PatchDocumentStyleWithTransformProp } from '../../../phantomjs.workaround';

import { SharedModule } from '../../shared/shared.module';
import { AuthLoginPageComponent } from './auth-login-page.component';
import { AuthService } from '../auth.service';
import { AuthDataService } from '../auth-data.service';

describe('AuthLoginPageComponent', () => {
  let component: AuthLoginPageComponent;
  let fixture: ComponentFixture<AuthLoginPageComponent>;

  beforeEach(async(() => {

    PatchDocumentStyleWithTransformProp();

    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        NoopAnimationsModule,

        FormsModule,
        MatInputModule,
        MatButtonModule,
        MatCheckboxModule,

        SharedModule
      ],
      declarations: [ AuthLoginPageComponent ],
      providers: [AuthService, AuthDataService]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthLoginPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
