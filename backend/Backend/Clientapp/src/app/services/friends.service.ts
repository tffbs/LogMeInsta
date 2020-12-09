import { Request } from './../model/request.model';
import { User } from './../model/user.model';
import { AppConfig } from './../config/config';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
@Injectable({
    providedIn: 'root'
  })
  export class FriendsService {

    private pathAPI = this.config.setting['PathAPI'] + 'user/';
  
    constructor(private http: HttpClient, private config: AppConfig) { }

    removeFriend(email: any): Observable<any> {
        let path = this.pathAPI + "removefriend";
        console.log(path);
        let params = new HttpParams();
        params = params.append('email', email);
        return this.http.get(path, {params: params})
    }

    addFriend(email: any): Observable<any> {
        let path = this.pathAPI + "addfriend";
        console.log(path);
        let params = new HttpParams();
        params = params.append('email', email);
        return this.http.get(path, {params: params})
    }

    public getUsers(): Observable<User[]>{
        let path = this.pathAPI + "people";
        console.log(path);
        return this.http.get<User[]>(path);    
    }
  
    public getfriends(): Observable<User> {
        let path = this.pathAPI + "friends";
      console.log(path);
      return this.http.get<User>(path);
    }

    search(value: any) {
        throw new Error('Method not implemented.');
      }

      getRequests(): Observable<Request[]>{
        let path = this.pathAPI + "requests";
        console.log(path);
        return this.http.get<Request[]>(path);      
      }

      acceptOrDeclineRequest(requestId: string, accept: boolean) {
        let path = this.pathAPI + "acceptorreject";
        console.log(path);
        let params = new HttpParams();
        params = params.append('requestId', requestId);
        params = params.append('accepted', accept ? "true" : "false");
        return this.http.get(path, {params: params})
      }
  }
  