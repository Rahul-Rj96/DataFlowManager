import { Component, OnInit } from '@angular/core';
import { FormtypeService } from '../../services/formtype.service';
import { FormDataModel } from '../../models/form-data-model';
import { ActivatedRoute, Router } from '@angular/router';
import { UserIdNameModel } from '../../models/user-id-name-model';
import { Assignformmodel } from '../../models/assignformmodel';

@Component({
  selector: 'app-assign-forms',
  templateUrl: './assign-forms.component.html',
  styleUrls: ['./assign-forms.component.scss']
})
export class AssignFormsComponent implements OnInit {
  forms: Array<FormDataModel>;
  formName: string;
  users: Array<UserIdNameModel>;
  itemSet: { [key: number]: number } = {};
  assignValues: Array<Assignformmodel>;

  constructor(private formTypeService: FormtypeService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.getFormsToAssign();
  }
  isEnabled() {
    // tslint:disable-next-line: triple-equals
    if (this.forms.length != 0) {
      return true;
    }
  }
  getFormsToAssign(): void {
    this.route
      .queryParams
      .subscribe(params => {
        this.formName = params.id || null;
      });
    this.formTypeService.getFormsToAssign(this.formName)
    .subscribe((result) => {
      this.forms = result;
      this.forms.forEach((item) => {
        this.itemSet[item.FormId] = null;
      });
    }
    );
    this.formTypeService.getUsersToAssign(this.formName)
    .subscribe((result) => {
      this.users = result;
    }
    );

  }

  onSubmit() {
    this.assignValues = [];
    this.forms.forEach((item) => {
      this.assignValues.push(new Assignformmodel(item.FormId, this.itemSet[item.FormId]));
    });
    this.formTypeService.postUserFormsData(this.assignValues);
    window.location.reload();
  }

}
