import { AppConfig } from './../config/config';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private pathAPI = this.config.setting['PathAPI'] + 'Auth/';

  constructor(private http: HttpClient, private config: AppConfig) { }

  public signin(): Observable<any> {
    let path = this.pathAPI + "signin";
    console.log(path);
    return this.http.get(path, this.getReturnUrlHeader());
  }

  private getReturnUrlHeader(){
    let header = new HttpHeaders({ 'ReturnUrl': this.config.setting['PathAPI'] + 'profile' });
    return { headers: header };  }

}
