import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  @Output() toggleSidebarEvent: EventEmitter<boolean> = new EventEmitter<boolean>();
  constructor() { }
  isOpenSidebar = false;
  ngOnInit(): void {
  }

  toggleSidebar(){
    this.isOpenSidebar = !this.isOpenSidebar;
    this.toggleSidebarEvent.emit(this.isOpenSidebar);
  }

}
