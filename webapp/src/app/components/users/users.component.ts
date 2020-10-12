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
    { id: 11, name: 'Sanyi', isFriend: true },
    { id: 12, name: 'Feri', isFriend: true },
    { id: 13, name: 'Jani', isFriend: true },
    { id: 14, name: 'Kati', isFriend: true },
    { id: 15, name: 'Ivett', isFriend: false },
    { id: 16, name: 'Áron', isFriend: true },
    { id: 17, name: 'Peti', isFriend: true },
    { id: 18, name: 'Laci', isFriend: false },
    { id: 19, name: 'Gabi', isFriend: true },
    { id: 20, name: 'Iván', isFriend: false },
  ];
  ngOnInit(): void {}

  changeUserStatus(user) {
    console.log('changing friend status');
    this.users.find((x) => x.id == user).isFriend = !this.users.find(
      (x) => x.id == user
    ).isFriend;
    // FB.login();
  }
}
