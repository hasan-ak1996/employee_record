import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private router: Router,
    private accountService: AccountService
  ) { }
  canActivate(route: ActivatedRouteSnapshot,
     state: RouterStateSnapshot): 
     boolean 
     | UrlTree 
     | Observable<boolean 
     | UrlTree> 
     | Promise<boolean 
     | UrlTree> {
      let token = localStorage.getItem('token');
      if (token) {
        const helper = new JwtHelperService();
        const isExpired = helper.isTokenExpired(token);
        if(!isExpired){

        
          if(route.data['permission']){
            
            let permissions: Array<string> = this.accountService.getCurrentUser().Permissions;
            let routePermissions: Array<string> = route.data['permission'];
            let result =permissions.filter(p1 => routePermissions.some(p2 => p2 == p1));
            if(result.length == 0){
              this.router.navigate(['/account/login'], {
                queryParams: { returnUrl: state.url },
              });
              this.accountService.logout();
              return false;
            }
          }
          return true;
          
        }else{
          
          this.router.navigate(['/account/login'], {
            queryParams: { returnUrl: state.url },
          });
          this.accountService.logout();
          return false;
        }
      }
      this.router.navigate(['/account/login'], {
        queryParams: { returnUrl: state.url },
      });
      this.accountService.logout();
      return false;
  }
}
