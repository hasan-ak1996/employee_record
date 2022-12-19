import { Component, ViewChild } from '@angular/core';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  isOpenSidebar = false;
  dirApp:string = 'lr';
  currentLang = 'en'
  constructor() {
  }
  title = 'angular-base-project';
  toggleSidebar(event: any){
    this.isOpenSidebar = event;
  }
  
}
