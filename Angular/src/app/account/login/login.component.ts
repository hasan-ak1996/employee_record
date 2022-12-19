import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticateModel } from 'src/app/core/models/tokenAuth/AuthenticateModel';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  loginModel!: AuthenticateModel;
  invalidLogin!: boolean;
  invalidLoginMsg :string = '';
  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private router: Router,
    private activatedRouter: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.createLoginForm();
  }
  createLoginForm(){
    this.loginForm = this.formBuilder.group({
      userNameOrEmailAddress: ['',[Validators.required,Validators.email]],
      password: ['',Validators.required],
      rememberClient: [false]
    });
  }

  singIn(){
    if(this.loginForm.valid){
      this.accountService.login(this.loginForm.value).subscribe(result => {
        if(result){
          let returnUrl = this.activatedRouter.snapshot.queryParamMap.get('returnUrl');
          this.router.navigate(  [ returnUrl || '/']);
        }
        else{
          this.invalidLogin = true;
        }
      },err => {
        this.invalidLogin = true;
       // this.invalidLoginMsg = err;
        this.invalidLoginMsg = "Invalid username/email or password";
      })
    }else{
      this.validateAllFormFields(this.loginForm)
    }

  }
  validateAllFormFields(formGroup: FormGroup) {         //{1}
    Object.keys(formGroup.controls).forEach(field => {  //{2}
      const control = formGroup.get(field);             //{3}
      if (control instanceof FormControl) {             //{4}
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) {        //{5}
        this.validateAllFormFields(control);            //{6}
      }
    });
  }

}
