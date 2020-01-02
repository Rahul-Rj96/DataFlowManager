import { Component, OnInit, Input } from '@angular/core';
import { FormDataModel } from '../../models/form-data-model';
import { FormtypeService } from '../../services/formtype.service';

@Component({
  selector: 'app-formdetails',
  templateUrl: './formdetails.component.html',
  styleUrls: ['./formdetails.component.scss']
})
export class FormdetailsComponent implements OnInit {
  @Input() form: FormDataModel;

  constructor(private formTypeService: FormtypeService) { }

  ngOnInit() {
  }
  onDelete() {
    this.formTypeService.deleteFormData(this.form.FormId);
    window.location.reload();

  }

}
