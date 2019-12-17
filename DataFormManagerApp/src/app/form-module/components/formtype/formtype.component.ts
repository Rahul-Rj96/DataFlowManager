import { Component, OnInit } from '@angular/core';
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
  itemSet:{[key:string]:string}={};
  dataValue:Array<DataValueModel>;
  formData:FormDataModel;
  id:number;
  constructor(private formTypeService: FormtypeService) { }

  ngOnInit() {
    this.getFormType();
  }
  getFormType(): void {
    this.formTypeService.getFormType().subscribe((result) => {
      this.formType = result;
      this.formType.FormFields.forEach((item)=>{
        this.itemSet[item.Name]=null;
      })
    })
  }

  onSubmit(){
    
    this.dataValue=[];
    this.formType.FormFields.forEach((item)=>{
      this.dataValue.push(new DataValueModel(item.Name,this.itemSet[item.Name])) 
    })
    this.formData=new FormDataModel (this.formType.FormType,this.dataValue);
    this.formTypeService.postFormData(this.formData);
    console.log(this.formData);
    

  }

}
