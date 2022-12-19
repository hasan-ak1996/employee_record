import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeesComponent } from './employees.component';
import { RouterModule, Routes } from '@angular/router';
import { AddEmployeeComponent } from './add-employee/add-employee.component';
import { EmployeeDetailsComponent } from './employee-details/employee-details.component';
import { UpdateEmployeeComponent } from './update-employee/update-employee.component';

const routes: Routes = [
  {path: '' ,
   component: EmployeesComponent,
  },
  {path: 'add-employee' ,
  component: AddEmployeeComponent,
 },
 {path: 'employee-details/:id' ,
 component: EmployeeDetailsComponent,
},
 {path: 'update-employee/:id' ,
 component: UpdateEmployeeComponent,
},
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),
    CommonModule
  ]
})
export class EmployeeRoutingModule { }
