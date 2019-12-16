import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import { FormsModule } from '@angular/forms';


import { UserspecificformComponent } from './userspecificform/userspecificform.component';
import { FormdetailsComponent } from './formdetails/formdetails.component';
import { FormtypeComponent } from './formtype/formtype.component';
import {HeaderComponent} from '../header/header.component';
import {FooterComponent} from '../footer/footer.component';
import { FormRoutingModule } from './form-routing.module';
import { FormtypeService } from './formtype.service';
import { UserSpecificFormsService } from './userspecificform.service';


@NgModule({
  declarations: [
    FooterComponent,
    UserspecificformComponent,
    FormdetailsComponent,
    HeaderComponent,
    FormtypeComponent],
  imports: [
    FormsModule,
    HttpClientModule,
    CommonModule,
    FormRoutingModule
  ],
  providers:[FormtypeService,
    UserSpecificFormsService
  ]
})
export class FormModule { }
