import { Component, OnInit, Input } from '@angular/core';
import { FormDataModel } from '../../models/form-data-model';
import { FormtypeService } from '../../services/formtype.service';
import { Router, ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-formdetails',
  templateUrl: './formdetails.component.html',
  styleUrls: ['./formdetails.component.scss']
})
export class FormdetailsComponent implements OnInit {
  @Input() form: FormDataModel;

  constructor(private formTypeService: FormtypeService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
  }
  onDelete() {
    this.formTypeService.deleteFormData(this.form.FormId);
    //window.location.href = 'http://dataformmanager.dev37.grcdev.com/dashboard/forms/userspecificform?id=Leave';
   // this.router.navigate(['dashboard/forms/userspecificform'], { queryParams: { id: this.form.FormType } });
    window.location.reload();

  }

}
