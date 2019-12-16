import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import {Observable,of, throwError} from 'rxjs';
import { Cookie } from '../model/cookie';
import { catchError, retry,map } from 'rxjs/operators';
import { Token, CodeObject ,RefreshTokenObject} from '../model/token';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl: string = "http://dataformmanager.dev37.grcdev.com/api/login/"
  verifyUrl: string = "http://dataformmanager.dev37.grcdev.com/api/login/verifyCookie"
  accessTokenUrl: string = "http://dataformmanager.dev37.grcdev.com/api/login/AccessToken"
  RefreshTokenUrl: string = "http://dataformmanager.dev37.grcdev.com/api/login/RefreshToken"

  constructor(private cookieService: CookieService,private http:HttpClient) { }

  loggedIn(){ 

    return (!!localStorage.getItem('accessToken') || !!localStorage.getItem('refreshToken'))
      // return !!this.cookieService.get('session').split('SubKey=')[1];
  }

  verifyCookie(): Observable<string>{
    return this.http.get<string>(this.verifyUrl)
  }   

  getAccessTokenByCode(codeObj : CodeObject): Observable<Token>{
    return this.http.post<Token>(this.accessTokenUrl,codeObj)
  }  

  getAccesTokenByRefreshToken(RefreshTokenObj : RefreshTokenObject): Observable<Token>{
    return this.http.post<Token>(this.RefreshTokenUrl,RefreshTokenObj)
  }  
  getAccessTokenFromLocalStorage(){
    return localStorage.getItem('accessToken')
  }
  getRefreshTokenFromLocalStorage(){
    return localStorage.getItem('refreshToken')
  }
}
