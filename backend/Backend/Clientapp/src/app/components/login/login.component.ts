import { AuthService } from './../../services/auth.service';
import { Helpers } from './../../helpers/helpers';
import { Component, OnInit } from '@angular/core';

declare var FB: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  constructor(private helpers: Helpers, private auth: AuthService) {}

  ngOnInit() {
    this.loginDev();
  }

  loginDev() {
    this.helpers.authenticate();
    document.body.classList.remove('bg-img');
  }

  submitLogin() {
    console.log('submit login to facebook');
    this.auth.signin().subscribe(x =>
      {
        console.log(x)
        
      },
      // (error) => {
      //   window.location.href = error.url;
      // }
      );

  }
}
