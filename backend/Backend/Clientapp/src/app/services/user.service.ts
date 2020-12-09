import { User } from './../model/user.model';
import { AppConfig } from './../config/config';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
  })
  export class UserService {


    private pathAPI = this.config.setting['PathAPI'] + 'user/';

    constructor(private http: HttpClient, private config: AppConfig) { }


    public getInfo(): Observable<User> {
      let path = this.pathAPI + "userinfo";
      console.log(path);
      return this.http.get<User>(path);
    }

    getFeed(): Observable<any> {
      let path = this.pathAPI + "feed";
      console.log(path);
      return this.http.get<any>(path);
    }

    likePicture(uid: string) {
      let path = this.pathAPI + "like";
      console.log(path);
      let params = new HttpParams();
      params = params.append('uid', uid);
      return this.http.get(path, {params: params})
    }

  }
