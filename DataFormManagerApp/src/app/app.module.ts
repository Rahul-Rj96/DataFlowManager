import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthService } from './auth.service';
import { AuthGuard } from './auth.guard';
import { CookieService } from 'ngx-cookie-service';
import { TokenInterceptorService } from './token-interceptor.service';
import { CommonComponentModule } from './common-components/common.module';
import { GlobalErrorHandler } from './global-error-handler.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


@NgModule({
  declarations: [
    AppComponent  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    CommonComponentModule,
    BrowserAnimationsModule
  ],
  providers: [ AuthService, AuthGuard, CookieService,
  {
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptorService,
    multi: true
  },
  {
    provide: ErrorHandler,
    useClass: GlobalErrorHandler
  }
],
  bootstrap: [AppComponent]
})
export class AppModule { }
