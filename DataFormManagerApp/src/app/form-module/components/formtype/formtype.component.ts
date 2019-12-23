import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormTypeModel } from '../../../model/form-type-model';
import { FormtypeService } from '../../services/formtype.service';
import { DataValueModel } from '../../../model/data-value-model';
import { FormDataModel } from '../../../model/form-data-model';



@Component({
  selector: 'app-formtype',
  templateUrl: './formtype.component.html',
  styleUrls: ['./formtype.component.scss']
})
export class FormtypeComponent implements OnInit {
  formType: FormTypeModel;
  itemSet: { [key: string]: string } = {};
  dataValue: Array<DataValueModel>;
  formData: FormDataModel;
  options: Array<string>;
  formTypeId: string;
  constructor(private formTypeService: FormtypeService, private route: ActivatedRoute) { }

  ngOnInit() {
    
    this.getFormType();
  }
  getFormType(): void {
  this.route
    .queryParams
    .subscribe(params => {
      this.formTypeId = params['id'] || 0;
    });
    this.formTypeService.getFormType(this.formTypeId).subscribe((result) => {
      this.formType = result;
      this.formType.FormFields.forEach((item) => {
        this.itemSet[item.Name] = null;
      })
    })
  }

  onSubmit() {
    this.dataValue = [];
    this.formType.FormFields.forEach((item) => {
      this.dataValue.push(new DataValueModel(item.Name, this.itemSet[item.Name]))
    })
    this.formData = new FormDataModel(this.formType.FormType, this.dataValue);
    this.formTypeService.postFormData(this.formData);
    window.location.reload();
  }

}
