import { Component, OnInit } from '@angular/core';
import { FormDataModel } from '../../../model/form-data-model';
import { Router, ActivatedRoute } from '@angular/router';
import { UserSpecificFormsService } from '../../services/userspecificform.service';


@Component({
  selector: 'app-userspecificform',
  templateUrl: './userspecificform.component.html',
  styleUrls: ['./userspecificform.component.scss']
})
export class UserspecificformComponent implements OnInit {
  username = localStorage.getItem('Username');
  forms: FormDataModel;
  selectedForm: FormDataModel;
  formName: string;
  constructor(private userSpecificFormService: UserSpecificFormsService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.getForms();
  }
  getForms(): void {
    this.route
      .queryParams
      .subscribe(params => {
        this.formName = params.id || 0;
      });
    this.userSpecificFormService.getForms(this.formName).subscribe((result) => {
      this.forms = result;

    }
    );
  }

  onSelect(form: FormDataModel): void {
    this.selectedForm = form;
  }
}
