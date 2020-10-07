import { Helpers } from './../../helpers/helpers';
import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  subscription: Subscription;

  authentication: boolean = false;
  constructor(private helpers: Helpers) { }

  ngOnInit(): void {
  }

  ngAfterViewInit() {
    this.subscription = this.helpers.isAuthenticated().subscribe(x => this.authentication = true)
  }
  
  title = 'webapp';
}
