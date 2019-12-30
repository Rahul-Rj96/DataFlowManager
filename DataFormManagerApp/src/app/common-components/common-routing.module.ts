import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProfileComponent } from './profile/profile.component';
import { ErrorComponent } from './error/error.component';


const routes: Routes = [
  { path: 'profile' , component: ProfileComponent},
  { path: 'error' , component: ErrorComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CommonComponentRoutingModule { }
 