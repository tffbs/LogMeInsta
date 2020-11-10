import { AppConfig } from './../config/config';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  private pathAPI = this.config.setting['PathAPI'] + 'categories/';

  private imagesBase64: Array<String> = new Array<String>();

  constructor(private http: HttpClient, private config: AppConfig) { }

  public onUpload(file): Observable<any> {
    this.imagesBase64.push(file);
    return this.getMockObservable();
  }

  public getImgesInBase64() {
    return this.imagesBase64;
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