import { Helpers } from './../../helpers/helpers';
import { SearchService } from './../../services/search.service';
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

  constructor(private formBuilder: FormBuilder, private searchService: SearchService, private helpers: Helpers) { }

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
    this.searchService.search(value);
  }

  get f() { return this.searchForm.controls; }

  logout(){
    this.helpers.logout()
  }

}
