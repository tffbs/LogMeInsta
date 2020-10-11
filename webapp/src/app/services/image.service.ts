import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  private pathAPI = 'http://localhost:50498';

  constructor(private http: HttpClient) { }

  onUpload(file): Observable<any> {
    // this.http is the injected HttpClient
    this.delay(1000);
    return this.http.post(this.pathAPI + '/file-upload', file);
  }

  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
  }
}
