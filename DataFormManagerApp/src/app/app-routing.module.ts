import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './login/login.component';
import { UserspecificformComponent } from './userspecificform/userspecificform.component';
import { AuthGuard } from './auth.guard';


const routes: Routes = [
  { path: 'login' , component: LoginComponent},
  { path: 'userspecificform' ,component: UserspecificformComponent , canActivate: [AuthGuard]},
  { path: 'dashboard' , component: DashboardComponent, canActivate: [AuthGuard]},
  { path: '', redirectTo: '/login', pathMatch: 'full' },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }