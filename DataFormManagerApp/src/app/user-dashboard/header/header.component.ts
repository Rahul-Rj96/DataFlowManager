import { Component, OnInit ,Input} from '@angular/core';
import {Router} from "@angular/router"
import { AuthService } from 'src/app/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  name= localStorage.getItem('Username');
  isLoggedIn: boolean;

  constructor(private _router: Router, private _authService: AuthService) { }

  ngOnInit() {
    this.isLoggedIn = this._authService.loggedIn();
    console.log(this.isLoggedIn)
  }

  Logout(){
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    this._router.navigate(['/login'])
  }
  
  isUserLoggedIn(){
    return this._authService.loggedIn();
  }


}
