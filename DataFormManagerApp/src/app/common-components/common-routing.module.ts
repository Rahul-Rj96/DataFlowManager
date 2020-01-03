import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProfileComponent } from './profile/profile.component';
import { ErrorComponent } from './error/error.component';
import { CalendarComponent } from './calendar/calendar.component';


const routes: Routes = [
  { path: 'profile' , component: ProfileComponent},
  { path: 'error' , component: ErrorComponent},
  { path: 'calendar' , component: CalendarComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CommonComponentRoutingModule { }
 