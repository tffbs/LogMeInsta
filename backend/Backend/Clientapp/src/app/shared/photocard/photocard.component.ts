import { Picture } from './../../model/picture.model';
import { UserService } from './../../services/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { PhotoCard } from '../../models/photo';

@Component({
  selector: 'app-photocard',
  templateUrl: './photocard.component.html',
  styleUrls: ['./photocard.component.css']
})

export class PhotocardComponent implements OnInit {

  @Input() picture: Picture;
  @Input() username: string;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
  }

  upload(uid: string){
    this.userService.likePicture(this.picture.uid).subscribe(x => console.log(x));
    this.picture.likes = this.picture.likes + 1;
  }

}
