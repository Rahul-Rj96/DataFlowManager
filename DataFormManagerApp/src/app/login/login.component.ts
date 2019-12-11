import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private httpClient: HttpClient,
    private _authService: AuthService,private cookie:CookieService,
    private _router: Router) { }

  baseUrl: string = "http://dataformmanager.dev37.grcdev.com/"
  ngOnInit() {

    if (this._authService.loggedIn()) {
      this._router.navigate(['/userspecificform'])
    } 
  }


  onLogin() {
    // this.httpClient.get(this.baseUrl + "api/login/getauthcode").subscribe();
    window.location.href = this.baseUrl + "api/login/getauthcode";
  }
}
