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

  private formDataUrl = AppSettings.baseUrl + 'form/';

  getFormType(id: string): Observable<FormTypeModel> {
    return this.http.get<FormTypeModel>(this.formTypeUrl + id);
  }
  postFormData(formData: FormDataModel) {
    this.http.post(this.formDataUrl + 'add', formData).subscribe();
  }
  putFormData(formData: FormDataModel) {
    this.http.put(this.formDataUrl + 'update', formData).subscribe();
  }
  deleteFormData(id: number) {
    this.http.delete(this.formDataUrl + id).subscribe();
  }
}
