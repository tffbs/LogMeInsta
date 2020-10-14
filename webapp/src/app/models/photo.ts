export class PhotoCard{

    image: string;
    username: string;

    constructor(image, username) {
        this.image = image;
        this.username = username;
        
    }

}

export const PHOTOS: PhotoCard[] = [
    {image: 'assets/img/ph1.jpg', username: 'DevUser'},
    {image: 'assets/img/ph2.jpg', username: 'DevUser'},
    {image: 'assets/img/ph3.jpg', username: 'DevUser'},
    {image: 'assets/img/ph4.jpg', username: 'DevUser'},
];