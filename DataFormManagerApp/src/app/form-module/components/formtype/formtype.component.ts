import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormTypeModel } from '../../models/form-type-model';
import { FormtypeService } from '../../services/formtype.service';
import { UserSpecificFormsService } from '../../services/userspecificform.service';
import { DataValueModel } from '../../models/data-value-model';
import { FormDataModel } from '../../models/form-data-model';



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
  submit: boolean;
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
    if (this.FormId == 0) {

      this.formTypeService.getFormType(this.formTypeId).subscribe((result) => {
        this.formType = result;


        this.formType.FormFields.forEach((item) => {
          this.itemSet[item.Name] = null;
        });
        this.submit = true;
      });
    } else {
      this.formTypeService.getFormType(this.formTypeId).subscribe((result) => {
        this.formType = result;
      });
      this.userSpecificFormService.getForms(this.formTypeId).subscribe((result) => {
        result.forEach((item) => {
          if (item.FormId == this.FormId) {
            this.form = item;
          }
        });
        this.form.FormData.forEach((item) => {
          this.itemSet[item.Name] = item.Value;
        });
      }
      );
      this.submit = false;
    }
  }

  isEnabled() {
    return this.submit;
  }


  onSubmit() {
    this.dataValue = [];
    this.formType.FormFields.forEach((item) => {
      this.dataValue.push(new DataValueModel(item.Name, this.itemSet[item.Name]));
    });
    this.formData = new FormDataModel(this.formType.FormType, this.dataValue);
    this.formTypeService.postFormData(this.formData);
    this.router.navigate(['dashboard/forms/userspecificform'], { queryParams: { id: this.formTypeId } });
  }

  onSave() {
    this.dataValue = [];
    this.formType.FormFields.forEach((item) => {
      this.dataValue.push(new DataValueModel(item.Name, this.itemSet[item.Name]));
    });
    this.formData = new FormDataModel(this.formType.FormType, this.dataValue, this.form.FormId);
    this.formTypeService.putFormData(this.formData);
    this.router.navigate(['dashboard/forms/userspecificform'], { queryParams: { id: this.formTypeId } });
   }

}
