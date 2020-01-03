import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CommonComponentModule } from '../common-components/common.module';

import {MatTableModule, } from '@angular/material/table';
import {
  MatPaginator,
  MatSort,
  MatSelectModule,
  MatTooltipModule,
  MatInputModule,
  MatSortHeader
} from '@angular/material';
import { FormdetailsComponent } from '../form-module/components/formdetails/formdetails.component';

@NgModule({
  declarations: [DashboardComponent,
    MatPaginator,
    MatSort,
    MatSortHeader,
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    CommonComponentModule,
    MatTableModule,
    MatSelectModule,
    MatTooltipModule,
    MatInputModule,
  ]
})
export class DashboardModule { }
