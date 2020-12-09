import * as signalR from '@aspnet/signalr';
import {Predicate} from '@angular/core';
import {Message} from '../Message';

export class Signalr {
  private  hubConnection: signalR.HubConnection;
  constructor(url: string) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(url, {skipNegotiation: true, transport: signalR.HttpTransportType.WebSockets})
      .build();
  }
  // tslint:disable-next-line:typedef
  register(methodname: string, method: Predicate<Message>){
    this.hubConnection.on(methodname, method);
  }
  // tslint:disable-next-line:typedef
  start(){
    this.hubConnection
      .start()
      .then( () => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }
}
