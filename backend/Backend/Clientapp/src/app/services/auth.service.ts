import { AppConfig } from './../config/config';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private pathAPI = this.config.setting['PathAPI'] + 'auth/';

  constructor(private http: HttpClient, private config: AppConfig) { }


  public signin(): Observable<any> {
    let path = this.pathAPI + "signin";
    console.log(path);
    return this.http.get(path, this.getReturnUrlHeader());
  }

  public listfriends(): Observable<any> {
    let path = this.config.setting['PathAPI'] + "user/friends";
    console.log(path);
    return this.http.get(path, this.getReturnUrlHeader2(path));
  }

  private getReturnUrlHeader(){
    let header = new HttpHeaders({ 'ReturnUrl': 'http://localhost:4200/'});
    return { headers: header };  }

    private getReturnUrlHeader2(url: string){
      let header = new HttpHeaders({ 'ReturnUrl': url});
      return { headers: header };  }

}
