import { NgModule } from '@angular/core';
import { FormtypeComponent } from './formtype/formtype.component';
import { UserspecificformComponent } from './userspecificform/userspecificform.component';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../auth.guard';

const routes: Routes = [
  { path: '', component:FormtypeComponent ,canActivate: [AuthGuard]},
  { path: 'form' , component: FormtypeComponent,canActivate: [AuthGuard]},
  { path: 'userspecificform' ,component: UserspecificformComponent,canActivate: [AuthGuard]},
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports:[RouterModule]
})
export class FormRoutingModule { }
