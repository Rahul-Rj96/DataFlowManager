import { Injectable } from '@angular/core';
import {FormDataModel} from '../model/form-data-model';
import {HttpClient,HttpHeaders} from '@angular/common/http';
import {Observable,of} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserSpecificFormsService {

  constructor(private http:HttpClient) { }
  private usfUrl='http://dataformmanager.dev19.grcdev.com/api/userSpecificforms/1';
  getForms():Observable<FormDataModel>{
    return this.http.get<FormDataModel>(this.usfUrl)
  }
}
