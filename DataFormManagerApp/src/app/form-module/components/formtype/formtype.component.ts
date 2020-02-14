import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormTypeModel } from '../../models/form-type-model';
import { FormtypeService } from '../../services/formtype.service';
import { UserSpecificFormsService } from '../../services/userspecificform.service';
import { DataValueModel } from '../../models/data-value-model';
import { FormDataModel } from '../../models/form-data-model';
import { Datesmodel } from '../../models/datesmodel';
import { FILE } from 'dns';



@Component({
  selector: 'app-formtype',
  templateUrl: './formtype.component.html',
  styleUrls: ['./formtype.component.scss']
})
export class FormtypeComponent implements OnInit {
  formType: FormTypeModel;
  form: FormDataModel;
  itemSet: { [key: string]: string } = {};
  dataValue: Array<DataValueModel>;
  formData: FormDataModel;
  options: Array<string>;
  formTypeId: string;
  FormId: number;
  savemode: boolean;
  effectiveDates: Datesmodel;
  StartDate: string;
  EndDate: string;
  FormName: string;
  //File: File;
  fileId: string = null;
  // tslint:disable-next-line: max-line-length
  constructor(private formTypeService: FormtypeService, private userSpecificFormService: UserSpecificFormsService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {

    this.getFormType();
  }

  getFormType(): void {
    this.route
      .queryParams
      .subscribe(params => {
        this.formTypeId = params.id || 0;
        this.FormId = params.formId || 0;

      });
    this.formTypeService.getFormType(this.formTypeId).subscribe((result) => {
      this.formType = result;
      this.FormName = this.formType.FormName;
      if (this.FormId == 0) {
        this.formType.FormFields.forEach((item) => {
          this.itemSet[item.Id] = null;
          this.savemode = false;
        });
      } else {
        this.userSpecificFormService.getForms(this.formTypeId).subscribe((result) => {
          result.forEach((item) => {
            if (item.FormId == this.FormId) {
              this.form = item;
            }
          });
          this.form.FormData.forEach((item) => {
            this.itemSet[item.Name] = item.Value;
            if (item.Name == 'File') {
              console.log(this.fileId);
              this.fileId = item.Value;
              console.log(this.fileId);
            }
          });
        }
        );
        this.savemode = true;
      }
    });
  }

  isEnabled() {
    return this.savemode;
  }


  onSubmit() {
    this.dataValue = [];
    this.formType.FormFields.forEach((item) => {
      this.dataValue.push(new DataValueModel(item.Id, this.itemSet[item.Id]));
    });
    this.dataValue.forEach((item) => {
      if (item.Name == this.formType.EffectiveDate.StartDate) {
        this.StartDate = item.Value;
      }
      if (item.Name == this.formType.EffectiveDate.EndDate) {
        this.EndDate = item.Value;
      }
    });
    this.effectiveDates = new Datesmodel(this.StartDate, this.EndDate);
    if (this.savemode == false) {
      this.formData = new FormDataModel(this.formType.FormType, this.itemSet[this.FormName], this.dataValue, this.effectiveDates);
      this.formTypeService.postFormData(this.formData);
      window.location.reload();
    } else {
      // tslint:disable-next-line: max-line-length
      this.formData = new FormDataModel(this.formType.FormType, this.itemSet[this.FormName], this.dataValue, this.effectiveDates, this.form.FormId);
      this.formTypeService.putFormData(this.formData);
      this.router.navigate(['dashboard/forms/userspecificform'], { queryParams: { id: this.formTypeId } });
    }

  }

  onFileUpload(fieldId: string, event) {
    var File = event.target.files as File;
    let filedata: FormData = new FormData();
    filedata.append(this.formType.FormType, File[0], File.name);
    if (this.fileId == null) {
      this.formTypeService.postFile(filedata).subscribe((res) => {
        this.fileId = res as string;
        this.itemSet[fieldId] = res as string;
      });
    } else {
      this.formTypeService.putFile(filedata, this.fileId);
    }

  }
}
