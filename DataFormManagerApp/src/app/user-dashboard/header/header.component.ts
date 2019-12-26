import { Component, OnInit , Input} from '@angular/core';
import {Router} from '@angular/router';
import { AuthService } from 'src/app/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  name = localStorage.getItem('Username');

  constructor(private _router: Router) { }

  ngOnInit() {
  }

  Logout() {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    this._router.navigate(['/login']);
  }



}
