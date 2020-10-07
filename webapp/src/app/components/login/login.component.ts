import { Helpers } from './../../helpers/helpers';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private helpers: Helpers) { }

  ngOnInit(): void {
  }

  login(){
    this.helpers.authenticate();
  }

}
