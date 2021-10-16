import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { Login } from 'src/app/shared/models/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  userLogin: Login = {
    email: '',
    password: ''
  };

  constructor(private authService: AuthenticationService, private router: Router) { }

  ngOnInit(): void {
  }

  loginSubmit(form: NgForm) {
    // capture the email/password from the view
    // then send the model to Authentication Service

    // console.log(form);
    // console.log('login button clicked!');
    // console.log(this.userLogin);
    this.authService.login(this.userLogin)
      // if token is saved successfully, then redirect to home page
      // if error then show error message and stay on same page

      .subscribe(
        (response) => {
          if (response) {
            this.router.navigateByUrl('/');
          }
          (err: HttpErrorResponse) => {
            console.log(err);
          }
        })

  }

}