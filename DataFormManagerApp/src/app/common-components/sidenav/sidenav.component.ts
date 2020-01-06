import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth.service';


@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnInit {
  name: string; //localStorage.getItem('Username');
  role: string;
  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.authService.$isRoleSet.subscribe(res => {
      this.role = res;
    });

    this.authService.$isNameSet.subscribe(res => {
      this.name = res;
    });

  }


  isUserLoggedIn() {
    return this.authService.loggedIn();
  }

}
