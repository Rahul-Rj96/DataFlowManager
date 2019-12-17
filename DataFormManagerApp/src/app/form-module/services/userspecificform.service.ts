import { Injectable } from '@angular/core';
import {FormDataModel} from '../../model/form-data-model';
import {HttpClient,HttpHeaders} from '@angular/common/http';
import {Observable,of} from 'rxjs';
import { AppSettings } from '../../utils/app-settings';


@Injectable({
  providedIn: 'root'
})
export class UserSpecificFormsService {
  userId: number =  2;
  constructor(private http:HttpClient) { }
  private usfUrl= AppSettings.baseUrl+'userSpecificforms/'+this.userId;
  getForms():Observable<FormDataModel>{
    return this.http.get<FormDataModel>(this.usfUrl)
  }
}
