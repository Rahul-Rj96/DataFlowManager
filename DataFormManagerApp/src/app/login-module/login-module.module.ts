import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';

import { LoginModuleRoutingModule } from './login-module-routing.module';
import { AuthService } from '../auth.service';
import { CommonComponentModule } from '../common-components/common.module';




@NgModule({
  declarations: [
    LoginComponent
  ],

  imports: [

    CommonModule,
    LoginModuleRoutingModule,
    CommonComponentModule
  ],
  providers: [AuthService
  ]
})
export class LoginModuleModule { }
