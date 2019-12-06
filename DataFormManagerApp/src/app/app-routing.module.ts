import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './login/login.component';
import { UserspecificformComponent } from './userspecificform/userspecificform.component';


const routes: Routes = [
  { path: 'login' , component: LoginComponent},
  { path: 'userspecificform' ,component: UserspecificformComponent},
  { path: 'dashboard' , component: DashboardComponent},
  { path: '', redirectTo: '/login', pathMatch: 'full' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

