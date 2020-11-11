import { AuthService } from './../../services/auth.service';
import { SearchService } from './../../services/search.service';
import { Component, OnInit } from '@angular/core';
import { ToastService } from './../../services/toast.service';

export class AppModule {}
@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
})
export class UsersComponent implements OnInit {
  constructor(private toastService: ToastService, private searchService: SearchService, private authService: AuthService) {}
  title = 'Search Users';
  searchText;
  users = [
    {
      id: 11,
      name: 'Ivett',
      isFriend: 'assets/img//remove.png',
      profilePicture: 'assets/img/profile_pictures/ivett.png',
    },
    {
      id: 12,
      name: 'Csaba',
      isFriend: 'assets/img//add.png',
      profilePicture: 'assets/img/profile_pictures/sanyi.png',
    },
    {
      id: 13,
      name: 'Jani',
      isFriend: 'assets/img//add.png',
      profilePicture: 'assets/img/profile_pictures/jani.png',
    },
    {
      id: 14,
      name: 'Kati',
      isFriend: 'assets/img//remove.png',
      profilePicture: 'assets/img/profile_pictures/kati.png',
    },
  ];
  ngOnInit(): void {}

  changeUserStatus(user) {
    console.log('changing friend status');
    if (
      this.users.find((x) => x.id == user).isFriend === 'assets/img//remove.png'
    ) {
      // this.toastService.show('Friend removed.', {
      //   classname: 'bg-danger text-light',
      //   delay: 2000,
      // });
      this.users.find((x) => x.id == user).isFriend = 'assets/img//add.png';
    } else {
      // this.toastService.show('Friend added.', {
      //   classname: 'bg-success text-light',
      //   delay: 2000,
      // });
      this.users.find((x) => x.id == user).isFriend = 'assets/img//remove.png';
    }
    // FB.login();
  }

  getFriends(){
    this.authService.listfriends().subscribe(x => console.log(x));
  }
}
