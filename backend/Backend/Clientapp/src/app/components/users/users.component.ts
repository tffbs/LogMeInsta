import { Subscription } from 'rxjs';
import { User } from './../../model/user.model';
import { FriendsService } from './../../services/friends.service';
import { AuthService } from './../../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { ToastService } from './../../services/toast.service';

export class AppModule {}
@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
})
export class UsersComponent implements OnInit {
  
  title = 'Search Users';
  searchText;
  users: User[] = [];

  constructor(private toastService: ToastService, private authService: AuthService, private friendsService: FriendsService) {}
  
  ngOnInit(): void {
    this.getFriends()
    this.friendsService.getUsers().subscribe(x => {
      (x != null && x.length > 0) ? this.users = x : console.log("no users found")
      console.log(this.users)
    }
      );
  }

  changeUserStatus(user) {
    console.log('changing friend status');
    if(user.isFriend){
      this.friendsService.removeFriend(user.email).subscribe(x => console.log(x));
      this.toastService.show('Friend removed.', {
        classname: 'bg-danger text-light',
        delay: 2000,
      });
    }else{
      this.friendsService.addFriend(user.email).subscribe(x => console.log(x));
      this.toastService.show('Friend request sent.', {
        classname: 'bg-success text-light',
        delay: 2000,
      });
    }
    user.isFriend = !user.isFriend;
  }

  getFriends(){
    this.friendsService.getfriends().subscribe(
      x => console.log(x),
      (error) => {
        console.log(error);
        window.location.href = error.url
      } 
      );
  }

  getUsers(){
    this.friendsService.getUsers().subscribe(
      x => console.log(x),
      (error) => window.location.href = error.url
      );
  }
}
