import { UserService } from './../../services/user.service';
import { FriendsService } from './../../services/friends.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { User } from 'src/app/model/user.model';
import { PhotoCard } from 'src/app/models/photo';
import { Router } from '@angular/router';
import { ImageService } from 'src/app/services/image.service';


@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit, OnDestroy {


  user: User[];

  cardDetails: Array<PhotoCard>;

  constructor(private imageService: ImageService, private friendsService: FriendsService, private userService: UserService, private router: Router) {  }


  ngOnInit(): void {
    this.userService.getFeed().subscribe(x => {
      this.user = x;
    });
  }

  ngOnDestroy(): void {
  }


}
