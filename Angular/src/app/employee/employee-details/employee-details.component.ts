import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { employeeDto } from 'src/app/core/models/employees/employeeDto';
import { EmployeeService } from '../employee.service';

@Component({
  selector: 'app-employee-details',
  templateUrl: './employee-details.component.html',
  styleUrls: ['./employee-details.component.scss']
})
export class EmployeeDetailsComponent implements OnInit {
  employee: employeeDto | undefined;
  employeeId!: number;
  constructor(
    private activatedroute: ActivatedRoute,
    private employeeService: EmployeeService
  ) { }

  ngOnInit(): void {
    this.activatedroute.paramMap.subscribe((params) => {

      this.employeeId =parseInt(params.get('id') || "");
      if(!isNaN(this.employeeId)){
        this.employeeService.getByIdWithNavigartionProperty(this.employeeId).subscribe((res) => {
          this.employee = res;
        })
      }else{
      }
    });
  }

}
