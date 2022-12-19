import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/auth/auth-guard';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
//{path: '' , loadChildren : () => import('./home/home.module').then(mod => mod.HomeModule) },
  {path: '' , loadChildren : () => import('./employee/employee.module').then(mod => mod.EmployeeModule) },
 // {path: 'account' , loadChildren : () => import('./account/account.module').then(mod => mod.AccountModule) },
  {path: '**' , redirectTo: '', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
