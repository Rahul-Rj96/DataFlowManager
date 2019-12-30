import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CommonComponentRoutingModule } from './common-routing.module';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { SidenavComponent } from './sidenav/sidenav.component';
import { ProfileComponent } from './profile/profile.component';
import { ErrorComponent } from './error/error.component';


@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    SidenavComponent,
    ProfileComponent, 
    ErrorComponent],
  imports: [
    CommonModule,
    CommonComponentRoutingModule
  ],
  exports:[
    HeaderComponent,
    FooterComponent,
    SidenavComponent,
    ProfileComponent,
    ErrorComponent
  ]
})
export class CommonComponentModule { }
