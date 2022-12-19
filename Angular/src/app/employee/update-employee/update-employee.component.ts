import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { departmentDto } from 'src/app/core/models/departments/departmentDto';
import { employeeFilesDto } from 'src/app/core/models/employeeFiles/employeeFilesDto';
import { employeeDto } from 'src/app/core/models/employees/employeeDto';
import { DepartmentService } from 'src/app/department/department.service';
import { EmployeeFilesService } from 'src/app/employee-files/employee-files.service';
import Swal from 'sweetalert2';
import { EmployeeService } from '../employee.service';

@Component({
  selector: 'app-update-employee',
  templateUrl: './update-employee.component.html',
  styleUrls: ['./update-employee.component.scss'],
  providers:[DatePipe ]
})
export class UpdateEmployeeComponent implements OnInit {
  employee: employeeDto | undefined;
  employeeId!: number;
  employeeForm:any;
  isSubmitForm = false;
  departments:departmentDto[]=[];
  deletedEmployeeFiles: employeeFilesDto[]=[];
  selectedImages:File[] = [];
  selectedImagesUrls : string[] = [];
  constructor(
    private activatedroute: ActivatedRoute,
    private employeeService: EmployeeService,
    private fb: FormBuilder,
    private departmentService: DepartmentService,
    public rout:Router,
    private employeeFilesService: EmployeeFilesService,
    private datePipe: DatePipe
  ) { }

  ngOnInit(): void {
    this.getAllDepartments();
    this.createForm();
    this.activatedroute.paramMap.subscribe((params) => {

      this.employeeId =parseInt(params.get('id') || "");
      if(!isNaN(this.employeeId)){
        this.employeeService.getByIdWithNavigartionProperty(this.employeeId).subscribe((res) => {
          this.employee = res;
          this.createForm(this.employee);
        })
      }else{
      }
    });
  }
  getAllDepartments(){
    this.departmentService.getAllDepartments().subscribe((res)=>{
      this.departments = res;
    })
  }
  createForm(employee?: employeeDto){
    var t= this.datePipe.transform(employee?.dateOfBirth,'MM/dd/yyyy');
    console.log(t,"t")
    this.employeeForm = this.fb.group({
      id:[employee?.id],
      name:[employee?.name,[Validators.required]],
      departmentId:[employee?.departmentId],
      dateOfBirth: [this.convertStringToDate(employee?.dateOfBirth),[Validators.required]],
      address:[employee?.address],
      deletedEmployeeFiles:[]
    });
  }

  convertStringToDate(str:string | undefined){
    if(str){
      var parts=str.split('/');
      return new Date(parts[0]+"-"+parts[1]+"-"+parts[2]);
    }else{
      return;
    }

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
  removeNewImage(index: number){
    this.selectedImages = Array.from(this.selectedImages).filter((item,i) => {
      return i != index
    });
    this.selectedImagesUrls = this.selectedImagesUrls.filter((item,i) => {
      return i != index
    });
  }
  removeOldImage(image:employeeFilesDto){
    this.deletedEmployeeFiles.push(image);
    if(this.employee){
      this.employee.employeeFiles = this.employee?.employeeFiles.filter((item) => {
        return item != image;
      })
    }

  }
  submit(){
    this.isSubmitForm = true;
    if(this.employeeForm.valid){
      console.log(this.employeeForm.get('dateOfBirth')?.value.toLocaleString())
      this.employeeForm.get('deletedEmployeeFiles')?.setValue(this.deletedEmployeeFiles);
      this.employeeService.updateEmployee(this.employeeForm.value).subscribe((res) => {
        if(res){

          if(this.selectedImages.length != 0){
            let formData = new FormData();
            formData.append("EmployeeId",this.employeeForm.get('id')?.value.toString());
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
    }
  }

}
