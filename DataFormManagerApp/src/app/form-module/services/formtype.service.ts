import { Injectable } from '@angular/core';
import { FormTypeModel } from '../../model/form-type-model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { FormDataModel } from '../../model/form-data-model';
import { AppSettings } from '../../utils/app-settings';


@Injectable({
  providedIn: 'root'
})
export class FormtypeService {

  constructor(private http: HttpClient) { }
  private formTypeUrl = AppSettings.baseUrl+'formtype/Release';
  getFormType(): Observable<FormTypeModel> {
    return this.http.get<FormTypeModel>(this.formTypeUrl)
  }
  private formDataUrl= AppSettings.baseUrl+'form/adddata';
  postFormData(formData:FormDataModel){
     this.http.post(this.formDataUrl,formData).subscribe()

  }
}