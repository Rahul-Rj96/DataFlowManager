import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
// import {HttpClientModule} from '@angular/common/http';
import { FormsModule } from '@angular/forms';


import { UserspecificformComponent } from './components/userspecificform/userspecificform.component';
import { FormdetailsComponent } from './components/formdetails/formdetails.component';
import { FormtypeComponent } from './components/formtype/formtype.component';

import { FormRoutingModule } from './form-routing.module';
import { FormtypeService } from './services/formtype.service';
import { UserSpecificFormsService } from './services/userspecificform.service';


@NgModule({
  declarations: [
    UserspecificformComponent,
    FormdetailsComponent,
    FormtypeComponent],
  imports: [
    FormsModule,
    // HttpClientModule,
    CommonModule,
    FormRoutingModule
  ],
  providers:[FormtypeService,
    UserSpecificFormsService
  ]
}) 
export class FormModule { }
