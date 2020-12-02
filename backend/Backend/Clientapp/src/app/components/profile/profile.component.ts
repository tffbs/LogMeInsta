import { User } from './../../model/user.model';
import { UserService } from './../../services/user.service';
import { Request } from './../../model/request.model';
import { FriendsService } from './../../services/friends.service';
import { PHOTOS } from './../../models/photo';
import { Component, OnInit } from '@angular/core';
import { PhotoCard } from 'src/app/models/photo';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user: User;
  photos = PHOTOS;
  requests: Request[] = [];


  constructor(private friendsService: FriendsService, private userService: UserService) { }

  ngOnInit(): void {
    this.getRequests();
    this.getUserInfo();
  }

  
  getRequests(){
    this.friendsService.getRequests().subscribe(x => {
      this.requests = x
    },
    (error) => console.log(error));
  }

  getUserInfo(){
    this.userService.getInfo().subscribe(x => {
      this.user = x
      console.log(this.user)
    })
  }
}
