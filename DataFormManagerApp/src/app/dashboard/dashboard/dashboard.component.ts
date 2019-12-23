import { Component, OnInit } from '@angular/core';
import { FormtypeService } from 'src/app/form-module/services/formtype.service';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  username= localStorage.getItem('Username');
  constructor(private formTypeService: FormtypeService) { }

  ngOnInit() {
  }

}
