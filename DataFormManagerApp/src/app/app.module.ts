import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FooterComponent } from './footer/footer.component';
import { HeaderComponent } from './header/header.component';
<<<<<<< HEAD
import { CookieService }  from 'ngx-cookie-service';
=======
import { UserspecificformComponent } from './userspecificform/userspecificform.component';
import { FormdetailsComponent } from './formdetails/formdetails.component';

>>>>>>> 1897d07704fabe15e9a959e03c25bb5bf4f759ea

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
  providers: [CookieService],
  bootstrap: [AppComponent]
})
export class AppModule { }
