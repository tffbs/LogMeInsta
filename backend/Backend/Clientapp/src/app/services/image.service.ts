import { AppConfig } from './../config/config';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  private pathAPI = this.config.setting['PathAPI'] + 'user/';

  constructor(private http: HttpClient, private config: AppConfig) { }

  public onUpload(fileToUpload): Observable<any> {
    let path = this.pathAPI + "upload";
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    console.log(path)
    return this.http.post(path, formData, {reportProgress: true, observe: 'events'})
  }
}
