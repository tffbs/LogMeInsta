import { PHOTOS } from './../../models/photo';
import { Component, OnInit } from '@angular/core';
import { PhotoCard } from 'src/app/models/photo';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  profileUserName: string;
  numberOfUploads: number;
  numberOfFriends: number;


  photos = PHOTOS;

  constructor() { }

  ngOnInit(): void {
    this.profileUserName = "DevUser";
    this.numberOfFriends = 0;
    this.numberOfUploads = this.photos.length;
  }

}
