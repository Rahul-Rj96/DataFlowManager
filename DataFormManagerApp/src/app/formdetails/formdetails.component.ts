import { Component, OnInit ,Input} from '@angular/core';
import {Form} from '../form';

@Component({
  selector: 'app-formdetails',
  templateUrl: './formdetails.component.html',
  styleUrls: ['./formdetails.component.scss']
})
export class FormdetailsComponent implements OnInit {
  @Input() form:Form;

  constructor() { }

  ngOnInit() {
  }

}