import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { employeeDto } from '../core/models/employees/employeeDto';
import { pagedEmployeeResultRequestDto } from '../core/models/employees/pagedEmployeeResultRequestDto';
import { EmployeeService } from './employee.service';
import Swal from 'sweetalert2'
import { Router } from '@angular/router';
@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.scss']
})
export class EmployeesComponent implements OnInit {
  employees:employeeDto[] = [];
  filter= {
    skipCount : 0,
    maxResult: 10
  } as pagedEmployeeResultRequestDto;
  totalCount = 0;
  displayedColumns: string[] = ['name', 'dateOfBirth', 'address', 'departmentName','creationTime','actions'];
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private employeeService:EmployeeService,
    private router: Router) { }

  ngOnInit(): void {
    this.getAllEmployees(this.filter);
  }

  getAllEmployees(filter: pagedEmployeeResultRequestDto){
    this.employeeService.getAllEmployees(filter).subscribe((res) => {
      console.log(res)
      this.employees = res.items;
      this.totalCount = res.totalCount;
    })

  }
  changePage(event:any){
      this.filter.skipCount = event.pageIndex;
      console.log(event.pageIndex);
      this.getAllEmployees(this.filter);

  }
  selectedEmployee(employeeId:number){
    this.router.navigateByUrl('employee-details/'+employeeId);
  }
  search(){
    this.getAllEmployees(this.filter);
  }

  deleteEmployee(event:any,id:number){
    event.stopPropagation();

    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.employeeService.deleteEmployee(id).subscribe((res) => {
          if(res){

            Swal.fire(
              'Deleted!',
              'Your file has been deleted.',
              'success'
            ).then(() => {
              this.getAllEmployees(this.filter);
            })
          }else{
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Something went wrong!',
              footer: '<a href="">Why do I have this issue?</a>'
            })
          }
        });

      }
    })
  }
}
