import { Helpers } from './../../helpers/helpers';
import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  subscription: Subscription;

  isAuthenticated: boolean = true;
  constructor(private helpers: Helpers, private router: Router) { }

  ngOnInit(): void {
  }

  ngAfterViewInit() {
  }
  
  title = 'webapp';
}
