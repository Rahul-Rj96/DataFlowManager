import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserDashboardRoutingModule } from './user-dashboard-routing.module';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { SidenavComponent } from './sidenav/sidenav.component';
import { ProfileComponent } from './profile/profile.component';


@NgModule({
  declarations: [HeaderComponent, FooterComponent, SidenavComponent, ProfileComponent],
  imports: [
    CommonModule,
    UserDashboardRoutingModule
  ],
  exports:[HeaderComponent,FooterComponent,SidenavComponent,ProfileComponent]
})
export class UserDashboardModule { }
