import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { AuthGuard } from '../auth.guard';




const routes: Routes = [
  { path: 'login' , component: LoginComponent},
  {path:'forms',
  loadChildren:'../form-module/form.module#FormModule',},
  { path: '', redirectTo: '/login', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LoginModuleRoutingModule { }
