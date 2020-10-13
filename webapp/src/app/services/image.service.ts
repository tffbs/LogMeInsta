import { PhotoCard } from './../model/photo';
import { AppConfig } from './../config/config';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  private pathAPI = this.config.setting['PathAPI'] + 'categories/';

  private cardDetails: Array<PhotoCard> = new Array<PhotoCard>();

  public cardDetailsEventEmitter: EventEmitter<Array<PhotoCard>> = new EventEmitter();

  constructor(private http: HttpClient, private config: AppConfig) { }

  public onUpload(file): Observable<any> {
    let card = new PhotoCard(file, "Mock user");
    this.cardDetails.push(card);
    this.cardDetailsEventEmitter.next(this.cardDetails);
    return this.getMockObservable();
  }

  public getCardDetails() {
    return this.cardDetails;
  }

  private delay(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  private getMockObservable() {
    return Observable.create(obs => {
      setTimeout(() => {
        obs.next([1, 2, 3]);
        obs.complete();
      }, 500);
    });
  }
}
