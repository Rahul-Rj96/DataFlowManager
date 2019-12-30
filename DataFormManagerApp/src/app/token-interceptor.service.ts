import { Injectable, Injector } from '@angular/core';
import { HttpInterceptor, HttpErrorResponse, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { AuthService} from './auth.service';
import { catchError} from 'rxjs/operators';
import { throwError, Observable} from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { RefreshTokenObject , Token} from './models/token';
import { AppSettings } from './utils/app-settings';
@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService implements HttpInterceptor {

  constructor(private injector: Injector) { }
  intercept(request, next): Observable<HttpEvent<any>> {

    const authService = this.injector.get(AuthService);
    if (authService.getAccessTokenFromLocalStorage()) {
      request = this.addToken(request, authService.getAccessTokenFromLocalStorage());
  }
    return next.handle(request).pipe(catchError( error => {
      if (error instanceof HttpErrorResponse && error.status === 401) {
        return this.handle401Error(request, next);
      } else {
        return throwError(error);
      }
    }));
  }

  private addToken(request: HttpRequest<any>, token: string) {
    return request.clone({
      setHeaders: {
        // tslint:disable-next-line: object-literal-key-quotes
        'Authorization': `Bearer ${token}`
      }
    });
  }



private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
    const authService = this.injector.get(AuthService);
    if (authService.getRefreshTokenFromLocalStorage()) {
    var refreshTokenObj: RefreshTokenObject = new RefreshTokenObject();
    var tokenObj: Token = new Token();
    refreshTokenObj.RefreshToken = authService.getRefreshTokenFromLocalStorage();
    return authService.getAccesTokenByRefreshToken(refreshTokenObj).pipe(
      switchMap((token: Token) => {
        tokenObj = token;
        localStorage.setItem('accessToken', tokenObj.AccessToken);
        localStorage.setItem('refreshToken', tokenObj.RefreshToken);
        localStorage.setItem('expiresIn', tokenObj.ExpiresIn);
        localStorage.setItem('userId', tokenObj.UserId.toString());
        localStorage.setItem('Username', tokenObj.Username);
        localStorage.setItem('EmailId', tokenObj.EmailId);
        return next.handle(this.addToken(request, authService.getAccessTokenFromLocalStorage()));
      }));


  } else {
    window.location.href = AppSettings.baseUrl + 'api/login/getauthcode';
  }
}
}






// let authService = this.injector.get(AuthService)
//     const accessToken = authService.getTokenFromLocalStorage();
//     var tokenizeReq = req.clone();
//     if (accessToken){
//       tokenizeReq = req.clone({
//       setHeaders: {
//         Authorization: `Bearer ${authService.getTokenFromLocalStorage()}`
//       }
//     })
//   }
//     //return next.handle(tokenizeReq)
//     return next.handle(tokenizeReq).pipe(catchError(error => {
//       if (error instanceof HttpErrorResponse && error.status === 401) {
//         return this.handle401Error(tokenizeReq, next);
//       } else {
//         return throwError(error);
//       }
//     }));
//   }
