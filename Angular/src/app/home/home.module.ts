import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { HomeRoutingModule } from './home-routing.module';


@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    MatCheckboxModule,
    HomeRoutingModule
  ]
})
export class HomeModule { }
