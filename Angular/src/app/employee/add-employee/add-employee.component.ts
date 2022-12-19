import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { departmentDto } from 'src/app/core/models/departments/departmentDto';
import { DepartmentService } from 'src/app/department/department.service';
import { EmployeeService } from '../employee.service';
import Swal from 'sweetalert2'
import { Route, Router } from '@angular/router';
import { EmployeeFilesService } from 'src/app/employee-files/employee-files.service';
@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.scss']
})
export class AddEmployeeComponent implements OnInit {
  departments:departmentDto[]=[];
  employeeForm!:any;
  isSubmitForm = false;
  selectedImages:File[] = [];
  selectedImagesUrls : string[] = [];
  constructor(
    private departmentService: DepartmentService,
    private fb: FormBuilder,
    private employeeService:EmployeeService,
    public rout:Router,
    private employeeFilesService: EmployeeFilesService) { 
      this.createForm();
    }
  
  ngOnInit(): void {
    this.getAllDepartments();
    this.createForm();
  }
  createForm(){
    this.employeeForm = this.fb.group({
      name:['',[Validators.required]],
      departmentId:[null],
      dateOfBirth: ['',[Validators.required]],
      address:['']
    });
  }
  getAllDepartments(){
    this.departmentService.getAllDepartments().subscribe((res)=>{
      this.departments = res;
    })
  }
  onChangeFile(event: any){
    this.selectedImages =  event.files;
    for(var file of this.selectedImages){
      const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = async (_event) => {
      await  this.selectedImagesUrls.push(reader.result as string) ;
    };
    }
  }
  removeImage(index: number){
    this.selectedImages = Array.from(this.selectedImages).filter((item,i) => {
      return i != index
    });
    this.selectedImagesUrls = this.selectedImagesUrls.filter((item,i) => {
      return i != index
    });
  }
  submit(){
    this.isSubmitForm = true;
    if(this.employeeForm.valid){
      this.employeeService.createEmployee(this.employeeForm.value).subscribe((res) => {
        if(res != -1){
          if(this.selectedImages.length != 0){
            let formData = new FormData();
            formData.append("EmployeeId",res.toString());
            Array.from(this.selectedImages).forEach((item) => {
              formData.append("Files",item,item.name);
            });
            this.employeeFilesService.createListEmployeeFiles(formData).subscribe((res) =>{
              if(res){
                Swal.fire('Saved Successfully', '', 'success').then(() =>{
                  this.rout.navigateByUrl('')
                });
              }else{
                Swal.fire({
                  icon: 'error',
                  title: 'Oops...',
                  text: 'Something went wrong!',
                  footer: '<a href="">Why do I have this issue?</a>'
                })
              }
            })

          }else{
            Swal.fire('Saved Successfully', '', 'success').then(() =>{
              this.rout.navigateByUrl('')
            });
          }


        }else{
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!',
            footer: '<a href="">Why do I have this issue?</a>'
          })
        }
      })
    }
  }

}
