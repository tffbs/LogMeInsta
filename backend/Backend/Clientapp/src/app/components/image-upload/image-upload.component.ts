import { ToastService } from './../../services/toast.service';
import { ImageService } from './../../services/image.service';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-image-upload',
  templateUrl: './image-upload.component.html',
  styleUrls: ['./image-upload.component.css']
})
export class ImageUploadComponent implements OnInit {

  selectedFileBase64: string;
  loading = false;
  message;

  constructor(private imageService: ImageService, private toastService: ToastService) { }

  ngOnInit(): void {
  }


  onFileChanged(event) {
    this.getBase64(event.target.files[0]);
  }

  getBase64(file: File) {
    let reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function () {
      //me.modelvalue = reader.result;
      console.log(reader.result);
    };
    reader.onerror = function (error) {
      console.log('Error: ', error);
    };  
    reader.onload = () => {
      this.selectedFileBase64 = reader.result as string;
    };
  }

  onUpload() {
    if (this.selectedFileBase64) {
      this.loading = true;
      this.imageService.onUpload(this.selectedFileBase64)
        .subscribe(x => {
          this.loading = false;
          this.toastService.show('The image was successfully uploaded.', { classname: 'bg-success text-light', delay: 2000 });

        },
          (error) => {
            this.loading = false;
            this.toastService.show('An error occured while uploading the image.', { classname: 'bg-danger text-light', delay: 2000 });
          }
        );
    } else {
      this.toastService.show('Please provide an image.', { classname: 'bg-danger text-light', delay: 2000 });
    }
  }

}
