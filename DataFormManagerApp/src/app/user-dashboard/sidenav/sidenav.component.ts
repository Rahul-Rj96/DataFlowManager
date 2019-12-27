import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { JwtHelperService } from "@auth0/angular-jwt";


@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnInit {
  name = localStorage.getItem('Username');

  constructor(private _authService: AuthService) { }

  ngOnInit() {
  }


  isUserLoggedIn() {
    return this._authService.loggedIn();
  }

  getPermission(formname: string, permission: string) {
    var token = localStorage.getItem('accessToken');

    const helper = new JwtHelperService();

    const decodedToken = helper.decodeToken(token);
    const expirationDate = helper.getTokenExpirationDate(token);
    const isExpired = helper.isTokenExpired(token);
    for (var formPermission of decodedToken.role) {
      var jsonObj = JSON.parse(formPermission)
      if (jsonObj.FormName == formname) {
        var permissionValue = this.getPermissionValue(jsonObj.Permission);

      }
    }
    var permissionValueObj = {
      'Read': 1,
      'Write': 2,
      'FullAccess': 3
    }
    if (permissionValue >= permissionValueObj[permission]) {
      return true
    }
    else {
      return false;

    }

  }

  getPermissionValue(permission: string) {
    var permissionValueObj = {
      'Read': 1,
      'Write': 2,
      'FullAccess': 3
    }
    var totalPermissionValue = 0;
    for (let key in permissionValueObj) {
      if (key == permission) {
        totalPermissionValue += permissionValueObj[key]
      }
    }
    return totalPermissionValue;
  }
}
