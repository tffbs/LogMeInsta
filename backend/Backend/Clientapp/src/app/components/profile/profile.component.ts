import { ToastService } from './../../services/toast.service';
import { Subscription } from 'rxjs';
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


  constructor(private friendsService: FriendsService, private userService: UserService, private toastService: ToastService) { }

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
    },
    (error) => window.location.href = error.url
    )
  }

  acceptRequest(requestId: string){
    this.friendsService.acceptOrDeclineRequest(requestId, true).subscribe(
      x => {
        this.toastService.show('Accepted request', { classname: 'bg-success text-light', delay: 2000 });
      }
    );
  }

  declineRequest(requestId: string){
    this.friendsService.acceptOrDeclineRequest(requestId, false).subscribe(x => {
      this.toastService.show('Declined request', { classname: 'bg-danger text-light', delay: 2000 });

    });
  }
}
