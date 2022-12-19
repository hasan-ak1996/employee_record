import { Component, Input, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { PermissionCheckerService } from '../../auth/permission-checker.service';
import { MenuItem } from '../../layout/menu-item';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss']
})
export class SideBarComponent implements OnInit {

  constructor(private permissionCheckerService: PermissionCheckerService,
    private translateService: TranslateService) { }
  @Input() isOpenSidebar = false;
  menuItems!: MenuItem[];

  ngOnInit(): void {
    this.menuItems = this.getMenuItems();
  }

  getMenuItems(): MenuItem[] {
    return [
        new MenuItem('Employees', '/', 'fa fa-users'),
        new MenuItem(
          'Users',
            '/example',
            'fa fa-users',
            'Pages.Users'
        ),
        new MenuItem(
          'Roles',
            '/Roles',
            'fa fa-theater-masks',
            'Pages.Roles'),

            new MenuItem(
              'Contact',
                '',
                'fa fa-theater-masks',
                'Pages.Roles',[
                  new MenuItem(
                    'example',
                      '/example',
                      'fa fa-theater-masks',
                      'Pages.Users'),
                      new MenuItem(
                        'Contact2',
                          '/Contact2',
                          'fa fa-theater-masks',
                          'Pages.Roles'),
                          new MenuItem(
                            'Contact3',
                              '/Contact3',
                              'fa fa-theater-masks',
                              'Pages.Roles'),

                ]),
    ];
  }


  isMenuItemVisible(item: MenuItem){
    if(!item.permissionName){
      return true;
    }
    return this.permissionCheckerService.isGuard(item.permissionName);
  }
}
