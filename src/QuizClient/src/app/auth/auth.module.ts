import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatInputModule, MatButtonModule, MatCheckboxModule } from '@angular/material';

import { SharedModule } from '../shared/shared.module';
import { AuthService } from './auth.service';
import { AuthDataService } from './auth-data.service';
import { AuthLoginPageComponent } from './auth-login-page/auth-login-page.component';
import { AuthRegisterPageComponent } from './auth-register-page/auth-register-page.component';

/**
 * Provides authentication functionality.
 */
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
    SharedModule
  ],
  declarations: [AuthLoginPageComponent, AuthRegisterPageComponent],
  providers: [AuthService, AuthDataService]
})
export class AuthModule { }
