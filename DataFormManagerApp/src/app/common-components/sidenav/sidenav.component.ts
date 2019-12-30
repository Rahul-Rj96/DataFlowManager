import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth.service';


@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnInit {
  name = localStorage.getItem('Username');
  role = "";
  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.role = this.authService.getRole();
  }


  isUserLoggedIn() {
    return this.authService.loggedIn();
  }

}
