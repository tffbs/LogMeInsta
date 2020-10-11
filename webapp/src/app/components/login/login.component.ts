import { Helpers } from './../../helpers/helpers';
import { Component, OnInit } from '@angular/core';

declare var FB: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  constructor(private helpers: Helpers) {}

  ngOnInit() {
    (window as any).fbAsyncInit = function () {
      FB.init({
        appId: '778458156056746',
        cookie: true,
        xfbml: true,
        version: 'v3.1',
      });
      FB.AppEvents.logPageView();
    };
    document.body.classList.add('bg-img');
    (function (d, s, id) {
      var js,
        fjs = d.getElementsByTagName(s)[0];
      if (d.getElementById(id)) {
        return;
      }
      js = d.createElement(s);
      js.id = id;
      js.src = 'https://connect.facebook.net/en_US/sdk.js';
      fjs.parentNode.insertBefore(js, fjs);
    })(document, 'script', 'facebook-jssdk');
  }

  loginDev() {
    this.helpers.authenticate();
    document.body.classList.remove('bg-img');
  }

  submitLogin() {
    console.log('submit login to facebook');
    // FB.login();
    FB.login((response) => {
      console.log('submitLogin', response);
      if (response.authResponse) {
        this.helpers.authenticate();

        document.body.classList.remove('bg-img');
        //login success
        //login success code here
        //redirect to home page
      } else {
        console.log('User login failed');
      }
    });
  }
}
