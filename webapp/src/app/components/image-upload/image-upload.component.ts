import { ImageService } from './../../services/image.service';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-image-upload',
  templateUrl: './image-upload.component.html',
  styleUrls: ['./image-upload.component.css']
})
export class ImageUploadComponent implements OnInit {

  selectedFile: File
  loading = false;
  message;

  constructor(private imageService: ImageService, private toaster: ToastrService) { }

  ngOnInit(): void {
  }


  onFileChanged(event) {
    this.selectedFile = event.target.files[0]
  }

  onUpload() {
    this.loading = true;
    this.imageService.onUpload(this.selectedFile)
    .subscribe(x => {
      this.loading = false;
      this.toaster.success("Image uploded")
    },
    (error) => {
      this.loading = false;
      this.toaster.error("Unsuccessful upload");
    } 
    );
  }

}
