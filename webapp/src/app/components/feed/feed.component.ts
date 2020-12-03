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
  cardDetailsSubscription: Subscription;
  

  constructor(private imageService: ImageService) {
    
  }


  ngOnInit(): void {
    this.cardDetails = this.imageService.getCardDetails();
    this.cardDetailsSubscription = this.imageService.cardDetailsEventEmitter.subscribe(x => this.cardDetails = x);
  }

  ngOnDestroy(): void {
    this.cardDetailsSubscription.unsubscribe();
  }

}
