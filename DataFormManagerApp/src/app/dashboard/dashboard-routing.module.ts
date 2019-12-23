import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {DashboardComponent} from './dashboard/dashboard.component';
import { AuthGuard } from '../auth.guard';



const routes: Routes = [
  { path: '', component:DashboardComponent ,canActivate: [AuthGuard]},
  {path:'forms',
  loadChildren:'../form-module/form.module#FormModule'}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
