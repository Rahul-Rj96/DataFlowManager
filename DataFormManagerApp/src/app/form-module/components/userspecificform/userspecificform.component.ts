import { Component, OnInit } from '@angular/core';
import {FormDataModel} from '../../../model/form-data-model';
import { UserSpecificFormsService} from '../../services/userspecificform.service';


@Component({
  selector: 'app-userspecificform',
  templateUrl: './userspecificform.component.html',
  styleUrls: ['./userspecificform.component.scss']
})
export class UserspecificformComponent implements OnInit {

  forms:FormDataModel;
  selectedForm:FormDataModel;
  constructor(private userSpecificFormService:UserSpecificFormsService) { }

  ngOnInit() {
    this.getForms();
  }
getForms():void{
  this.userSpecificFormService.getForms().subscribe((result) => 
  {
    this.forms=result;

  }
  )}

  onSelect(form:FormDataModel):void{
    this.selectedForm=form;
  }
}
