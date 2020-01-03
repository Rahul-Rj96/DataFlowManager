import { Component, OnInit ,ViewChild} from '@angular/core';
import { UserSpecificFormsService } from 'src/app/form-module/services/userspecificform.service';
import { MatTableDataSource } from '@angular/material/table';
import { FormDataModel } from 'src/app/form-module/models/form-data-model';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {animate, state, style, transition, trigger} from '@angular/animations';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class DashboardComponent implements OnInit {

  tableColumns  :  string[] = ['FormId', 'FormType','FormData','ViewDetail'];
  dataSource: MatTableDataSource<FormDataModel>;
  form: FormDataModel;
  expandedElement: FormDataModel | null;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private userSpecificFormService: UserSpecificFormsService) { }

  ngOnInit() {
    

    this.userSpecificFormService.getForms('all')
      .subscribe((res) => {
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      });

  
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  onSelect(form: FormDataModel): void {
    this.form = form;
  }

}
