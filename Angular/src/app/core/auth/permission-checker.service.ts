import { Injectable } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';

@Injectable({
  providedIn: 'root'
})
export class PermissionCheckerService {

  constructor(private accountService: AccountService) { }

  token = localStorage.getItem('token');
  
  isGuard(permissionName: string):boolean{
    if(this.token){
      let userPernissions:string[] = this.accountService.getCurrentUser().Permissions;
      let index = userPernissions.findIndex(p => p == permissionName);
      if(index == -1){
        return false;
      }
      else{
        return true;
      }
    }else{
      return false;
    }
  }

}
