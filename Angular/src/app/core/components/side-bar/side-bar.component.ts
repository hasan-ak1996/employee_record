import { Component, Input, OnInit } from '@angular/core';
import { MenuItem } from '../../layout/menu-item';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss']
})
export class SideBarComponent implements OnInit {

  constructor() { }
  @Input() isOpenSidebar = false;
  menuItems!: MenuItem[];

  ngOnInit(): void {
    this.menuItems = this.getMenuItems();
  }

  getMenuItems(): MenuItem[] {
    return [
        new MenuItem('Employees', '/', 'fa fa-users')];
  }



}
