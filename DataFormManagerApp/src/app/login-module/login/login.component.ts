import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../../auth.service';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { CodeObject, Token } from '../../models/token';
import { AppSettings } from 'src/app/utils/app-settings';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private httpClient: HttpClient,
              private authService: AuthService, private cookie: CookieService,
              private router: Router) { }


  ngOnInit() {

    const authorizationCode = window.location.href.split('code=')[1];
    if (authorizationCode) {
      let tokenObj: Token = new Token();
      const codeObj: CodeObject = new CodeObject();
      codeObj.code = authorizationCode;
      this.authService
        .getAccessTokenByCode(codeObj)
        .subscribe((token: Token) => {
          tokenObj = token;
          localStorage.setItem('accessToken', tokenObj.AccessToken);
          localStorage.setItem('refreshToken', tokenObj.RefreshToken);
          localStorage.setItem('expiresIn', tokenObj.ExpiresIn);
          localStorage.setItem('userId', tokenObj.UserId.toString());
          localStorage.setItem('Username', tokenObj.Username);
          localStorage.setItem('EmailId', tokenObj.EmailId);
          this.router.navigate(['dashboard']);
        });
    }

    if (this.authService.loggedIn()) {
      this.router.navigate(['dashboard']);
    } else {
      this.router.navigate(['/login']);
    }
  }

  onLogin() {
    window.location.href = AppSettings.baseUrl + 'login/getauthcode';
  }
}
