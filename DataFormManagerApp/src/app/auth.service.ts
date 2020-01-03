import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError, Subject } from 'rxjs';
import { Token, CodeObject, RefreshTokenObject } from './models/token';
import { AppSettings } from './utils/app-settings';
import { JwtHelperService } from '@auth0/angular-jwt';
import { error } from 'protractor';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private isRoleSet: Subject<string> = new Subject();
  public $isRoleSet: Observable<string> = this.isRoleSet.asObservable();
  permissionValueObj = {
    // tslint:disable-next-line: object-literal-key-quotes
    'Read': 1,
    // tslint:disable-next-line: object-literal-key-quotes
    'Write': 2,
    // tslint:disable-next-line: object-literal-key-quotes
    'FullAccess': 4
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
    return localStorage.getItem('accessToken')

  }
  getRefreshTokenFromLocalStorage() {
    return localStorage.getItem('refreshToken');
  }

  getRole() {

    const token = this.getAccessTokenFromLocalStorage();
    console.log(token);
    const helper = new JwtHelperService();
      const decodedToken = helper.decodeToken(token);
      for (const formPermission of decodedToken.role) {
        const jsonObj = JSON.parse(formPermission);
        this.isRoleSet.next(jsonObj.RoleName);
        if (jsonObj.RoleName) {
          return jsonObj.RoleName;
        } else {
          return 'No role is assigned';
        }
      }

  }

  isAdmin() {
    if (this.getRole() == 'admin') {
      return true;
    } else {
      return false;
    }
  }

  getPermission(formname: string, permission: string) {
    const token = this.getAccessTokenFromLocalStorage();
    const helper = new JwtHelperService();
      const decodedToken = helper.decodeToken(token);
      const expirationDate = helper.getTokenExpirationDate(token);
      const isExpired = helper.isTokenExpired(token);
      var permissionValue = 0;
      for (const formPermission of decodedToken.role) {
        const jsonObj = JSON.parse(formPermission);
        if (jsonObj.FormName == formname) {
          permissionValue = permissionValue + this.permissionValueObj[jsonObj.Permission];
        }
      }
      if (permissionValue >= this.permissionValueObj[permission]) {
        return true;
      } else {
        return false;
      }
  
  }
}
