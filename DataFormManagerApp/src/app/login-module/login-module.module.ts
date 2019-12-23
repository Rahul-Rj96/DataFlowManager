import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';

import { LoginModuleRoutingModule } from './login-module-routing.module';
import { AuthService } from '../auth.service';
import { UserDashboardModule } from '../user-dashboard/user-dashboard.module';




@NgModule({
  declarations: [
    LoginComponent
  ],
    
  imports: [
    
    CommonModule,
    LoginModuleRoutingModule,
    CommonModule,
    UserDashboardModule
  ],
  providers:[AuthService
  ]
})
export class LoginModuleModule { }
