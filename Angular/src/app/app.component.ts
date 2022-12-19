import { Component, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AccountService } from './account/account.service';
import { PermissionCheckerService } from './core/auth/permission-checker.service';
import { MenuItem } from './core/layout/menu-item';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  isOpenSidebar = false;
  dirApp:string = 'lr';
  currentLang = 'en'
  constructor(
    private accountService: AccountService) {
  }
  title = 'angular-base-project';
  toggleSidebar(event: any){
    this.isOpenSidebar = event;
  }
  
}
