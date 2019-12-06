import { Component, OnInit } from '@angular/core';
import {Form} from '../form';
import { UserSpecificFormsService} from '../userspecificform.service';

@Component({
  selector: 'app-userspecificform',
  templateUrl: './userspecificform.component.html',
  styleUrls: ['./userspecificform.component.scss']
})
export class UserspecificformComponent implements OnInit {

  forms:Form[];
  selectedForm:Form;
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

  onSelect(form:Form):void{
    this.selectedForm=form;
  }
}
