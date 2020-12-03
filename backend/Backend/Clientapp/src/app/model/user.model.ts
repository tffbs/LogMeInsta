import { Picture } from './picture.model';
export class User{
    firstName: string;
    lastName: string;
    email: string;
    profilePic: string;
    isFriend: boolean
    friends: User[];
    pictures: Picture[]
}