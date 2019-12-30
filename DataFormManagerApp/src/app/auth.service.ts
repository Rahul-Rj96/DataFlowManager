import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable} from 'rxjs';
import { Token, CodeObject , RefreshTokenObject} from './models/token';
import { AppSettings } from './utils/app-settings';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  permissionValueObj = {
    // tslint:disable-next-line: object-literal-key-quotes
    'Read': 1,
    // tslint:disable-next-line: object-literal-key-quotes
    'Write': 2,
    // tslint:disable-next-line: object-literal-key-quotes
    'FullAccess': 3
  };
  constructor(private http: HttpClient) { }

  loggedIn() {

    return (!!localStorage.getItem('accessToken') || !!localStorage.getItem('refreshToken'));
  }

  verifyCookie(): Observable<string> {
    return this.http.get<string>(AppSettings.baseUrl + 'login/verifyCookie');
  }

  getAccessTokenByCode(codeObj: CodeObject): Observable<Token> {
    return this.http.post<Token>(AppSettings.baseUrl + 'login/AccessToken', codeObj);
  }

  getAccesTokenByRefreshToken(RefreshTokenObj: RefreshTokenObject): Observable<Token> {
    return this.http.post<Token>(AppSettings.baseUrl + 'login/RefreshToken', RefreshTokenObj);
  }
  getAccessTokenFromLocalStorage() {
    return localStorage.getItem('accessToken');
  }
  getRefreshTokenFromLocalStorage() {
    return localStorage.getItem('refreshToken');
  }

  getRole() {
    const token = localStorage.getItem('accessToken') ;
    const helper = new JwtHelperService();
    const decodedToken = helper.decodeToken(token);

    for ( const formPermission of decodedToken.role) {
      const jsonObj = JSON.parse(formPermission);
      if (jsonObj.RoleName) {
        return jsonObj.RoleName;
      } else {
        return 'No role is assigned';
      }
    }

  }

  getPermission(formname: string, permission: string) {
    const token = localStorage.getItem('accessToken') ;
    const helper = new JwtHelperService();
    const decodedToken = helper.decodeToken(token);
    const expirationDate = helper.getTokenExpirationDate(token);
    const isExpired = helper.isTokenExpired(token);
    for ( const formPermission of decodedToken.role) {
      const jsonObj = JSON.parse(formPermission);
      if (jsonObj.FormName == formname) {
          var permissionValue = this.getPermissionValue(jsonObj.Permission);
      }
    }
    if (permissionValue >= this.permissionValueObj[permission]) {
      return true;
    } else {
      return false;
    }
  }

  getPermissionValue(permission: string) {
    let totalPermissionValue = 0;
    for (const key in this.permissionValueObj) {
        if (key == permission) {
          totalPermissionValue += this.permissionValueObj[key];
        }
      }
    return totalPermissionValue;
  }
}
