import { Injectable } from '@angular/core';
import { FormTypeModel } from '../models/form-type-model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { FormDataModel } from '../models/form-data-model';
import { AppSettings } from '../../utils/app-settings';
import { UserIdNameModel } from '../models/user-id-name-model';
import { Assignformmodel } from '../models/assignformmodel';


@Injectable({
  providedIn: 'root'
})
export class FormtypeService {
  constructor(private http: HttpClient) { }
  private formTypeUrl = AppSettings.baseUrl + 'formtype/';

  private formDataUrl = AppSettings.baseUrl + 'form/';

  private userFormsUrl = AppSettings.baseUrl + 'userforms';

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
  getFormsToAssign(formName: string): Observable<Array<FormDataModel>> {
    return this.http.get<Array<FormDataModel>>(this.formDataUrl + formName);
  }
  getUsersToAssign(formType: string): Observable<Array<UserIdNameModel>> {
    return this.http.get<Array<UserIdNameModel>>(this.formDataUrl + 'users/' + formType);
  }
  postUserFormsData(userFormsData: Array<Assignformmodel>) {
    this.http.post(this.userFormsUrl , userFormsData).subscribe();
  }
}
