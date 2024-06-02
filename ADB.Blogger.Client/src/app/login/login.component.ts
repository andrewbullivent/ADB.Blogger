import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { LoginService } from '../services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],

})
export class LoginComponent implements OnInit {


  constructor(
    private loginService: LoginService, 
    private formBuilder: FormBuilder, 
    private router: Router ){}

  public userSignedIn: boolean = false;

  ngOnInit(): void {
    this.loginService.isSignedIn()
    .subscribe(isSignedIn => this.userSignedIn = isSignedIn);
  }

  loginForm = this.formBuilder.group({
    email:'',
    password:'',
  });

  onSubmit(){
    if(!this.loginForm.value.email || !this.loginForm.value.password){
      throw new Error("No username or password")
    }

    this.loginService.login(this.loginForm.value.email, this.loginForm.value.password)
    .subscribe(() => {
           this.router.navigate(['/post']);
    });  
  }

  Logout() {
    this.loginService.logout()
    .subscribe((success:boolean) => {
      this.userSignedIn = !success;
      if(!success){
        console.warn("Failed to log out successfully");
      }
    });
  }
}
