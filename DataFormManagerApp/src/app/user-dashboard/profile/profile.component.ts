import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  name= localStorage.getItem('Username');
  email= localStorage.getItem('EmailId');
  role= "";

  constructor(private _authService: AuthService) { }

  ngOnInit() {
    this.role = this._authService.getRole();
  }

}
