import { UserService } from './../../services/user.service';
import { FriendsService } from './../../services/friends.service';
import { Subscription } from 'rxjs';
import { ImageService } from './../../services/image.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { PhotoCard } from 'src/app/models/photo';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit, OnDestroy {

  cardDetails: Array<PhotoCard>;

  constructor(private imageService: ImageService, private friendsService: FriendsService, private userService: UserService) {  }

  ngOnInit(): void {
    this.userService.getFeed().subscribe(x => console.log(x));
  }

  ngOnDestroy(): void {
  }


}
