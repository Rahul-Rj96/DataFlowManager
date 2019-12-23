import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import {HttpClientModule, HttpInterceptor, HTTP_INTERCEPTORS} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';


import { AuthService } from './auth.service';
import { AuthGuard } from './auth.guard';
import { CookieService } from 'ngx-cookie-service'
import { TokenInterceptorService } from './token-interceptor.service';
import { UserDashboardModule } from './user-dashboard/user-dashboard.module';
// import { LoginModuleModule } from './login-module/login-module.module';
// import { FormModule } from './form-module/form.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    // FormsModule,
    // FormModule,
    // LoginModuleModule
    UserDashboardModule
  ],
  providers: [ AuthService, AuthGuard, CookieService,
  {
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptorService,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
