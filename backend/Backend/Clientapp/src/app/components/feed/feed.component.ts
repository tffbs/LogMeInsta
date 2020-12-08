import { UserService } from './../../services/user.service';
import { FriendsService } from './../../services/friends.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { User } from 'src/app/model/user.model';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit, OnDestroy {

  user: User[];
  constructor(private friendsService: FriendsService, private userService: UserService) {  }

  ngOnInit(): void {
    this.userService.getFeed().subscribe(x => {
      this.user = x;
    });
  }

  ngOnDestroy(): void {
  }


}
