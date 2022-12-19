import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs';
import { AuthenticateModel } from '../core/models/tokenAuth/AuthenticateModel';
import { CurrentUser } from '../core/models/User/CurrentUser';
import { Auth } from '../core/url-api/url-api';
import { JwtHelperService } from '@auth0/angular-jwt';
@Injectable({
  providedIn: 'root'
})
export class AccountService {



  constructor(private http: HttpClient,
    private router: Router) { }

  login(input: AuthenticateModel){
    return this.http.post(Auth.login,input)
    .pipe(
      map(response => {
        let resp = JSON.parse(JSON.stringify(response));
        if(resp && resp.result){
          localStorage.setItem('token',resp.result.accessToken);
          return true
        }
        else{
          return false;
        }

      })
    );
  }


  logout(){
    localStorage.removeItem('token');
    this.router.navigate(['account/login']);
  }

    getCurrentUser(): CurrentUser{
    let token = localStorage.getItem('token')?.toString();
    if(token){
      const helper = new JwtHelperService();
      const decodedToken: any = helper.decodeToken(token);
      return new CurrentUser(
        decodedToken.id,
        decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/name'],
        decodedToken.lastName,
        decodedToken.fullName,
        decodedToken.email,
        decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
        undefined,
        decodedToken.phoneNumber,
        JSON.parse(decodedToken.Permissions)
        
        )
    }
    else{
      return new CurrentUser();
    }


  }


  isUserLoggedIn(){
    let token = localStorage.getItem('token')?.toString();
    if(token){
      return true;
    }
    return false;
  }

}
