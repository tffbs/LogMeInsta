import { EventEmitter } from '@angular/core';

export class Helpers{

    private authenticateEvent: EventEmitter<boolean> = new EventEmitter<boolean>();

    authenticate() {
      this.authenticateEvent.emit(true);
    }

    isAuthenticated(){
        return this.authenticateEvent;
    }
}