import { Component, Input, OnInit } from '@angular/core';
import { PhotoCard } from '../../model/photo';

@Component({
  selector: 'app-photocard',
  templateUrl: './photocard.component.html',
  styleUrls: ['./photocard.component.css']
})

export class PhotocardComponent implements OnInit {

  @Input() image: string;
  @Input() userName: string;

  constructor() { }

  ngOnInit(): void {
  }

}
