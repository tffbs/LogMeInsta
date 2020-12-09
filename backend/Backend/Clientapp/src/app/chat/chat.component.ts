import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import { AppConfig } from './../config/config';
import {HttpClient} from '@angular/common/http';
import {Message} from '../../Message';
import {Signalr} from '../signalr';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  private  router: Router;
  private token: string;
  private http: HttpClient;
  public Messages: Array<Message>;
  private  sr: Signalr;
  private email: string;
  private pathAPI = this.config.setting['PathAPI'];
  constructor(router: Router, http: HttpClient, private config: AppConfig, private route: ActivatedRoute) {
    this.route.params.subscribe(params => this.saveEmail(params['email']));
    this.router = router;
    this.http = http;
    this.Messages = new Array<Message>();

    console.log(this.email);
    const ms = new Message();
    ms.receiver = this.email;
      this.http.post<Message[]>(this.pathAPI  + 'api/Message/getall/', ms).subscribe(r => {
        this.Messages = r;
      });

      this.sr = new Signalr(this.pathAPI + 'chatHub');
      this.sr.register('NewMessage', t => {
        this.Messages.push(t);
        return true;
      });
      this.sr.start();
  }
  // tslint:disable-next-line:typedef



  sendMessage(message: HTMLInputElement) {
    const mess = new Message();
    mess.msg = message.value;
    mess.receiver = this.email;
    this.http.post(this.pathAPI + 'api/Message', mess).subscribe();
    message.value = '';
  }

  saveEmail(email: string){
    this.email = email;
  }

  ngOnInit(): void {
  }

}
