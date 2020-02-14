import { Injectable } from '@angular/core';
import { FormTypeModel } from '../models/form-type-model';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { FormDataModel } from '../models/form-data-model';
import { AppSettings } from '../../utils/app-settings';
import { UserIdNameModel } from '../models/user-id-name-model';
import { Assignformmodel } from '../models/assignformmodel';
import { Stream } from 'stream';


@Injectable({
  providedIn: 'root'
})
export class FormtypeService {
  fileId: string;
  constructor(private http: HttpClient) { }
  private formTypeUrl = AppSettings.baseUrl + 'formtype/';

  private formDataUrl = AppSettings.baseUrl + 'form/';

  private userFormsUrl = AppSettings.baseUrl + 'userforms';

  private fileUrl = AppSettings.baseUrl + 'file/';

  getFormType(id: string): Observable<FormTypeModel> {
    return this.http.get<FormTypeModel>(this.formTypeUrl + id);
  }
  postFormData(formData: FormDataModel) {
    this.http.post(this.formDataUrl + 'add', formData).subscribe(
      (response) => {
        window.alert(response);
      }
    );
  }
  putFormData(formData: FormDataModel) {
    this.http.put(this.formDataUrl + 'update', formData).subscribe(
      (response) => {
      window.alert(response);
    });
  }
  deleteFormData(id: number) {
    this.http.delete(this.formDataUrl + id).subscribe(
      (response) => {
        window.alert(response);
      });
  }
  getFormsToAssign(formName: string): Observable<Array<FormDataModel>> {
    return this.http.get<Array<FormDataModel>>(this.formDataUrl + formName);
  }
  getUsersToAssign(formType: string): Observable<Array<UserIdNameModel>> {
    return this.http.get<Array<UserIdNameModel>>(this.formDataUrl + 'users/' + formType);
  }
  postUserFormsData(userFormsData: Array<Assignformmodel>) {
    this.http.post(this.userFormsUrl , userFormsData).subscribe(
      (response) => {
        window.alert(response);
      }
    );
  }
  postFile(File: FormData) {
    return this.http.post(this.fileUrl + 'create', File);
  }
  putFile(File: FormData , fileId: string) {
    const params = new HttpParams().set('id', fileId);
    this.http.post(this.fileUrl + 'edit/id' , File , {params}).subscribe();
  }
  GetFile(fileId: string) {
    const params = new HttpParams().set('id', fileId);
    return this.http.get(this.fileUrl + 'read/id', {observe: 'response', responseType: 'blob', params});

  }
}
