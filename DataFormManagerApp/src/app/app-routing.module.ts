import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './auth.guard';


const routes: Routes = [
  {path:'',
  loadChildren:'./login-module/login-module.module#LoginModuleModule'},
  {path: 'dashboard',
  loadChildren: './dashboard/dashboard.module#DashboardModule'}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }