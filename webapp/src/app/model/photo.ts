export class PhotoCard{

    image: string;
    username: string;

    constructor(image, username) {
        this.image = image;
        this.username = username;
        
    }

}

export const PHOTOS: PhotoCard[] = [
    {image: 'src/assets/img/ph1.jpg', username: 'uploader'},
    {image: 'src/assets/img/ph2.jpg', username: 'uploader'},
    {image: 'src/assets/img/ph3.jpg', username: 'uploader'},
    {image: 'src/assets/img/ph4.jpg', username: 'uploader'},
];