import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { HttpClient } from '@angular/common/http';
import {Observable} from 'rxjs';
import { Token, CodeObject ,RefreshTokenObject} from './model/token';
import { AppSettings } from './utils/app-settings';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http:HttpClient) { }

  loggedIn(){ 

    return (!!localStorage.getItem('accessToken') || !!localStorage.getItem('refreshToken'))
      // return !!this.cookieService.get('session').split('SubKey=')[1];
  }

  verifyCookie(): Observable<string>{
    return this.http.get<string>(AppSettings.baseUrl+'login/verifyCookie')
  }   

  getAccessTokenByCode(codeObj : CodeObject): Observable<Token>{
    return this.http.post<Token>(AppSettings.baseUrl+'login/AccessToken',codeObj)
  }  

  getAccesTokenByRefreshToken(RefreshTokenObj : RefreshTokenObject): Observable<Token>{
    return this.http.post<Token>(AppSettings.baseUrl+'login/RefreshToken',RefreshTokenObj)
  }  
  getAccessTokenFromLocalStorage(){
    return localStorage.getItem('accessToken')
  }
  getRefreshTokenFromLocalStorage(){
    return localStorage.getItem('refreshToken')
  }
}
