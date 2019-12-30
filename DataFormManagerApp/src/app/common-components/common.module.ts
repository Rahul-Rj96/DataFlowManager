import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CommonComponentRoutingModule } from './common-routing.module';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { SidenavComponent } from './sidenav/sidenav.component';
import { ProfileComponent } from './profile/profile.component';


@NgModule({
  declarations: [HeaderComponent, FooterComponent, SidenavComponent, ProfileComponent],
  imports: [
    CommonModule,
    CommonComponentRoutingModule
  ],
  exports:[HeaderComponent,FooterComponent,SidenavComponent,ProfileComponent]
})
export class CommonComponentModule { }
