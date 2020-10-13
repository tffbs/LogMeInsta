import { Component, OnInit } from '@angular/core';

export class AppModule {}
@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
})
export class UsersComponent implements OnInit {
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
      this.users.find((x) => x.id == user).isFriend = 'assets/img//add.png';
    } else {
      this.users.find((x) => x.id == user).isFriend = 'assets/img//remove.png';
    }
    // FB.login();
  }
}
