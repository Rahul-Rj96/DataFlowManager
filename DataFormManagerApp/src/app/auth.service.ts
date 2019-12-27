import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { HttpClient } from '@angular/common/http';
import {Observable} from 'rxjs';
import { Token, CodeObject ,RefreshTokenObject} from './model/token';
import { AppSettings } from './utils/app-settings';
import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  permissionValueObj = {
    'Read':1,
    'Write':2,
    'FullAccess':3
  }
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

  getRole(){
    var token = localStorage.getItem('accessToken') ;
    const helper = new JwtHelperService();   
    const decodedToken = helper.decodeToken(token);
   
    for ( var formPermission of decodedToken.role){
      var jsonObj = JSON.parse(formPermission)
      if (jsonObj.RoleName){
        return jsonObj.RoleName
      }
      else{
        return "No role is assigned"
      }   
    }
   
  }

  getPermission(formname: string, permission: string){
    var token = localStorage.getItem('accessToken') ;
    const helper = new JwtHelperService();   
    const decodedToken = helper.decodeToken(token);
    const expirationDate = helper.getTokenExpirationDate(token);
    const isExpired = helper.isTokenExpired(token);
    for ( var formPermission of decodedToken.role){
      var jsonObj = JSON.parse(formPermission)
      if (jsonObj.FormName == formname){
          var permissionValue = this.getPermissionValue(jsonObj.Permission);
      }      
    }
    if (permissionValue >= this.permissionValueObj[permission]){
      return true
    }
    else{
      return false;
    }  
  }

  getPermissionValue(permission:string){
    var totalPermissionValue =0;
      for (let key in this.permissionValueObj){
        if (key==permission){
          totalPermissionValue+= this.permissionValueObj[key]
        }
      }
      return totalPermissionValue;
  }
}
