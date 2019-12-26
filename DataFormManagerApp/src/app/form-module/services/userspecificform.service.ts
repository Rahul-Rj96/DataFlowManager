import { Injectable } from '@angular/core';
import { FormDataModel } from '../../model/form-data-model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { AppSettings } from '../../utils/app-settings';


@Injectable({
  providedIn: 'root'
})
export class UserSpecificFormsService {
  constructor(private http: HttpClient) { }


  private usfUrl = AppSettings.baseUrl + 'userSpecificforms/';
  getForms(formName: string): Observable<Array<FormDataModel>> {
    return this.http.get<Array<FormDataModel>>(this.usfUrl + formName);
  }


}
