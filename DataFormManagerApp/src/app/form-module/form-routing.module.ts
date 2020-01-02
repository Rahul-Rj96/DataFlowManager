import { NgModule } from '@angular/core';
import { FormtypeComponent } from './components/formtype/formtype.component';
import { UserspecificformComponent } from './components/userspecificform/userspecificform.component';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../auth.guard';
import { AssignFormsComponent } from './components/assign-forms/assign-forms.component';

const routes: Routes = [
  { path: 'form' , component: FormtypeComponent, canActivate: [AuthGuard]},
  { path: 'userspecificform', component: UserspecificformComponent, canActivate: [AuthGuard]},
  { path: 'assignform', component: AssignFormsComponent, canActivate: [AuthGuard]},
  { path: '', redirectTo: 'form', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FormRoutingModule { }
