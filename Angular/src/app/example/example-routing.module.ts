import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ExampleComponent } from './example.component';
import { AuthGuard } from '../core/auth/auth-guard';
import { PermissionNames } from '../core/auth/PermissionNames';

const routes: Routes = [
  {path: '' ,
   component: ExampleComponent,
  canActivate: [AuthGuard],
   data : {
    permission: [PermissionNames.examplePemission]
  }
  },
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),
    CommonModule
  ],
  exports: [
    
  ]
})
export class ExampleRoutingModule { }
