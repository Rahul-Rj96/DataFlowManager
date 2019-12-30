import { Injectable, ErrorHandler, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable()
export class GlobalErrorHandler  {

  constructor(private injector: Injector) { }

  handleError(error: any){
    const router = this.injector.get(Router);
    console.log('Request URL: ${router.url');

    if (error instanceof HttpErrorResponse) {
      console.error('BackEnd returned status code:', error.status);
      console.error('Response body:', error.message);

    }else{
      console.error('An error occured', error.message);
    }
    window.alert(error.message);
    router.navigate(['error']);
  }
}
