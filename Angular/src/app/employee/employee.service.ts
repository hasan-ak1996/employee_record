import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { createEmployeeDto } from '../core/models/employees/createEmployeeDto';
import { employeeDto } from '../core/models/employees/employeeDto';
import { pagedEmployeeResultRequestDto } from '../core/models/employees/pagedEmployeeResultRequestDto';

import { updateEmployeeDto } from '../core/models/employees/updateEmployeeDto';
import { PagedResultDto } from '../core/models/PagedResultDto';
import { Employees } from '../core/url-api/url-api';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http:HttpClient) { }

  // getAllEmployees(input:pagedEmployeeResultRequestDto):Observable<employeeDto[]>{
  //   let params = new HttpParams();
  //   console.log(input)
  //   params.append("SkipCount",input.skipCount.toString());
  //   params.append("MaxResult",input.maxResult.toString());
  //   return this.http.get<any>(Employees.GetAll,{observe : 'response',params})
  //   .pipe(
  //     map(res => {
  //       return res.body;
  //     })
  //   );
  // }

  getAllEmployees(input:pagedEmployeeResultRequestDto):Observable<PagedResultDto<employeeDto>>{
    // let params = new HttpParams();
    // console.log(input)
    // params.append("SkipCount",input.skipCount.toString());
    // params.append("MaxResult",input.maxResult.toString());
    var params = "?SkipCount="+input.skipCount+"&MaxResult="+input.maxResult;
    if(input.keyword){
      params = params+"&keyword="+input.keyword;
    }

    return this.http.get<any>(Employees.GetAll+params);
  }


  createEmployee(input: createEmployeeDto):Observable<number>{
    return this.http.post<number>(Employees.CreateWithGetId,input);
  }
  updateEmployee(input: updateEmployeeDto):Observable<boolean>{
    return this.http.put<boolean>(Employees.Update,input);
  }
  deleteEmployee(id:number):Observable<boolean>{
    return this.http.delete<boolean>(Employees.Delete+"?id="+id);
  }
  getByIdWithNavigartionProperty(id:number):Observable<employeeDto>{
    return this.http.get<employeeDto>(Employees.GetByIdWithNavigartionProperty+"?id="+id);
  }
  
}
