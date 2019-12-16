import { Injectable } from '@angular/core';
import { FormTypeModel } from '../model/form-type-model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { FormDataModel } from '../model/form-data-model';


@Injectable({
  providedIn: 'root'
})
export class FormtypeService {

  constructor(private http: HttpClient) { }
  private formTypeUrl = 'http://dataformmanager.dev19.grcdev.com/api/formtype/Release';
  getFormType(): Observable<FormTypeModel> {
    return this.http.get<FormTypeModel>(this.formTypeUrl)
  }
  private formDataUrl='http://dataformmanager.dev19.grcdev.com/api/form/adddata';
  postFormData(formData:FormDataModel){
     this.http.post(this.formDataUrl,formData).subscribe()

  }
}