import { User } from './../model/user.model';
import { AppConfig } from './../config/config';
import { HttpClient } from '@angular/common/http';
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

    getFeed(): Observable<User> {
      let path = this.pathAPI + "feed";
      console.log(path);
      return this.http.get<User>(path);
    }
  
  }
  