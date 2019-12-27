import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { JwtHelperService } from "@auth0/angular-jwt";


@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnInit {
  name= localStorage.getItem('Username');
  role="";
  constructor(private _authService: AuthService) { }

  ngOnInit() {
    this.role = this._authService.getRole();
  }


  isUserLoggedIn(){
    return this._authService.loggedIn();
  }

}
