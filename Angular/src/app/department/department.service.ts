import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { departmentDto } from '../core/models/departments/departmentDto';
import { pagedDepartmentResultRequestDto } from '../core/models/departments/pagedDepartmentResultRequestDto';
import { Departments } from '../core/url-api/url-api';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  constructor(private http:HttpClient) { }

  getAllDepartments():Observable<departmentDto[]>{
    let filter = new pagedDepartmentResultRequestDto();
    return this.http.get<any>(Departments.GetAll+"?SkipCount="+filter.skipCount+"&MaxResult="+filter.maxResult);
  }
}
