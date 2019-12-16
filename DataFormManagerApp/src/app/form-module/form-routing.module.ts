import { NgModule } from '@angular/core';
import { FormtypeComponent } from './formtype/formtype.component';
import { UserspecificformComponent } from './userspecificform/userspecificform.component';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: '', component:FormtypeComponent },
  { path: 'form' , component: FormtypeComponent},
  { path: 'userspecificform' ,component: UserspecificformComponent},
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports:[RouterModule]
})
export class FormRoutingModule { }
