import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormTypeModel } from '../../models/form-type-model';
import { FormtypeService } from '../../services/formtype.service';
import { UserSpecificFormsService } from '../../services/userspecificform.service';
import { DataValueModel } from '../../models/data-value-model';
import { FormDataModel } from '../../models/form-data-model';
import { Datesmodel } from '../../models/datesmodel';



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
  effectiveDates: Datesmodel;
  StartDate: string;
  EndDate: string;
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
    this.dataValue.forEach((item) => {
      if (item.Name == 'ReleaseDate' || item.Name == 'From') {
        this.StartDate = item.Value;
      }
      if (item.Name == 'ReleaseDate' || item.Name == 'To') {
        this.EndDate = item.Value;
      }
    });
    this.effectiveDates = new Datesmodel(this.StartDate, this.EndDate);
    this.formData = new FormDataModel(this.formType.FormType, this.dataValue,  this.effectiveDates);
    console.log(this.formData);
    this.formTypeService.postFormData(this.formData);
    window.location.reload();
  }

  onSave() {
    this.dataValue = [];
    this.formType.FormFields.forEach((item) => {
      this.dataValue.push(new DataValueModel(item.Name, this.itemSet[item.Name]));
    });
    this.dataValue.forEach((item) => {
      if (item.Name == 'ReleaseDate' || item.Name == 'From') {
        this.StartDate = item.Value;
      }
      if (item.Name == 'ReleaseDate' || item.Name == 'To') {
        this.EndDate = item.Value;
      }
    });
    this.effectiveDates = new Datesmodel(this.StartDate, this.EndDate);
    this.formData = new FormDataModel(this.formType.FormType, this.dataValue, this.effectiveDates, this.form.FormId);
    console.log(this.formData);
    this.formTypeService.putFormData(this.formData);
    this.router.navigate(['dashboard/forms/userspecificform'], { queryParams: { id: this.formTypeId } });
   }

}
