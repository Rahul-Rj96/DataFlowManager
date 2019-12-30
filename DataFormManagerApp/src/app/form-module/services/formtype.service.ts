import { Injectable } from '@angular/core';
import { FormTypeModel } from '../models/form-type-model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { FormDataModel } from '../models/form-data-model';
import { AppSettings } from '../../utils/app-settings';


@Injectable({
  providedIn: 'root'
})
export class FormtypeService {
  constructor(private http: HttpClient) { }
  private formTypeUrl = AppSettings.baseUrl + 'formtype/';

  private formDataUrl = AppSettings.baseUrl + 'form/data';

  getFormType(id: string): Observable<FormTypeModel> {
    return this.http.get<FormTypeModel>(this.formTypeUrl + id);
  }
  postFormData(formData: FormDataModel) {
    this.http.post(this.formDataUrl, formData).subscribe();
  }
  putFormData(formData: FormDataModel) {
    this.http.put(this.formDataUrl, formData).subscribe();
  }
}
