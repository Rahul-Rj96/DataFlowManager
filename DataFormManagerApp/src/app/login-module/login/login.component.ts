import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../../auth.service';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { catchError, retry, map } from 'rxjs/operators';
import { CodeObject, Token } from '../../model/token';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private httpClient: HttpClient,
    private _authService: AuthService, private cookie: CookieService,
    private _router: Router) { }

  baseUrl: string = "http://dataformmanager.dev37.grcdev.com/"

  ngOnInit() {

    var authorizationCode = window.location.href.split("code=")[1];
    if (authorizationCode) {
      let tokenObj: Token = new Token();
      let codeObj: CodeObject = new CodeObject();
      codeObj.code = authorizationCode;
      this._authService
        .getAccessTokenByCode(codeObj)
        .subscribe((token: Token) => {
          tokenObj = token;
          localStorage.setItem('accessToken', tokenObj.AccessToken);
          localStorage.setItem('refreshToken', tokenObj.RefreshToken);
          localStorage.setItem('expiresIn', tokenObj.ExpiresIn);
          localStorage.setItem('userId', tokenObj.UserId.toString());
          localStorage.setItem('Username', tokenObj.Username);
          localStorage.setItem('EmailId', tokenObj.EmailId);
          this._router.navigate(['dashboard']);
        })
    }

    if (this._authService.loggedIn()) {
      this._router.navigate(['dashboard'])
    }
    else{
      this._router.navigate(['/login'])
    }
  }


  onLogin() {
    // this.httpClient.get(this.baseUrl + "api/login/getauthcode").subscribe();
    window.location.href = this.baseUrl + "api/login/getauthcode";
  }
}
