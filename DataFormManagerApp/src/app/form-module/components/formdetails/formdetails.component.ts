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
  Download() {
    this.formTypeService.GetFile(this.form.FormData[3].Value).subscribe((res) => {
       const filename = res.headers.get('content-disposition').split('=')[1];
       console.log(filename);
       const url = window.URL.createObjectURL(res.body);
       const a: any = document.createElement('a');
       document.body.appendChild(a);
       a.href = url;
       a.download = filename;
       a.click();
    });
  }

}
