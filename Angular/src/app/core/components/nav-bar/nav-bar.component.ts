import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  @Output() toggleSidebarEvent: EventEmitter<boolean> = new EventEmitter<boolean>();
  constructor(private translateService: TranslateService) { }
  isOpenSidebar = false;
  currentLang = '';
  ngOnInit(): void {
    this.currentLang = localStorage.getItem('lang') || 'en';
  }

  toggleSidebar(){
    this.isOpenSidebar = !this.isOpenSidebar;
    this.toggleSidebarEvent.emit(this.isOpenSidebar);
  }
  selectLang() {
    localStorage.setItem('lang', this.currentLang);

    location.reload();
    
  }

}
