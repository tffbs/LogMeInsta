import { FriendsService } from './../../services/friends.service';
import { AuthService } from './../../services/auth.service';
import { Helpers } from './../../helpers/helpers';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-head',
  templateUrl: './head.component.html',
  styleUrls: ['./head.component.css']
})
export class HeadComponent implements OnInit {
  isExpanded: boolean = false;
  searchForm: FormGroup;
  submitted;

  constructor(private formBuilder: FormBuilder, private friendsService: FriendsService, private helpers: Helpers, private authservice: AuthService) { }

  ngOnInit(): void {
    this.initForm();
  }

  initForm(){
    this.searchForm = this.formBuilder.group({
      value: ['', Validators.required],
  });
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  onSubmit(){
    let value = this.searchForm.controls.value.value;
    if (this.searchForm.invalid) {
      return;
  }
  this.submitted = true;
    this.friendsService.search(value);
  }

  get f() { return this.searchForm.controls; }

  logout(){
    this.authservice.logout().subscribe(x => console.log(x));
  }

}