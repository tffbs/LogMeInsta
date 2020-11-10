import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppConfig } from '../config/config';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  private pathAPI = this.config.setting['PathAPI'] + 'user/';

  search(value: any) {
    throw new Error('Method not implemented.');
  }

  constructor(private http: HttpClient, private config: AppConfig) { }


  getFriends(): Observable<any>{
    let path = this.pathAPI + "friends";
    console.log(path)
    return this.http.get<any>(path);
}



}
