import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EmployeeFiles } from '../core/url-api/url-api';

@Injectable({
  providedIn: 'root'
})
export class EmployeeFilesService {

  constructor(private http:HttpClient) { }
  createListEmployeeFiles(fromData: FormData):Observable<boolean>{
    return this.http.post<boolean>(EmployeeFiles.Create,fromData);
  }
}
