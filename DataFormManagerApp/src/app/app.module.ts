import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FooterComponent } from './footer/footer.component';
import { HeaderComponent } from './header/header.component';

import { UserspecificformComponent } from './userspecificform/userspecificform.component';
import { FormdetailsComponent } from './formdetails/formdetails.component';
import { AuthService } from './auth.service';
import { AuthGuard } from './auth.guard';
import { CookieService } from 'ngx-cookie-service'


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DashboardComponent,
    FooterComponent,
    HeaderComponent,
    UserspecificformComponent,
    FormdetailsComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule
  ],
  providers: [ AuthService, AuthGuard, CookieService],
  bootstrap: [AppComponent]
})
export class AppModule { }
