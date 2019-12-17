import { Component, OnInit, Input } from '@angular/core';
import { FormDataModel } from '../../../model/form-data-model';

@Component({
  selector: 'app-formdetails',
  templateUrl: './formdetails.component.html',
  styleUrls: ['./formdetails.component.scss']
})
export class FormdetailsComponent implements OnInit {
  @Input() form: FormDataModel;

  constructor() { }

  ngOnInit() {
  }

}