import { Injectable } from '@angular/core';
import {Form} from './form';
import {HttpClient,HttpHeaders} from '@angular/common/http';
import {Observable,of} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserSpecificFormsService {

  constructor(private http:HttpClient) { }
  private usfUrl='http://dataformmanager.dev37.grcdev.com/api/userSpecificforms/1';
  getForms():Observable<Form[]>{
    return this.http.get<Form[]>(this.usfUrl)
    
  }
}
