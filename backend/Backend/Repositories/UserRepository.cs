using Backend.Data;
using Backend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Backend.Repositories
{
    public class UserRepository :IUserRepository
    {
        ApplicationDbContext context;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            this.context = applicationDbContext;
        }

        public bool AddFriendRequest(string email, ApplicationUser currentUser)
        {
            ApplicationUser friend = this.context.ApplicationUsers.Where(x => x.Email == email).FirstOrDefault();;
            if (friend.Id != null && !friend.Requests.Any(x => x.Creator == currentUser.Email))
            {
                //Add the request in his/her friend request list.
                FriendRequest newRequest = new FriendRequest()
                {
                    Creator = currentUser.Email,
                    Time = DateTime.Now,
                    UID = Guid.NewGuid().ToString()
                };

                friend.Requests.Add(newRequest);
                currentUser.Friends.Add(friend);
                this.context.SaveChanges();

                return true;
            }
            return false;
        }

        public void RemoveFriend(string email, ApplicationUser currentUser)
        {
            try
            {
                var userToRemove = this.context.ApplicationUsers.Where(x => x.Email == email).FirstOrDefault();
                currentUser.Friends.Remove(userToRemove);
            }catch(Exception e)
            {
                //log error
            }

        }

        public FriendRequest GetUserRequest(string requestId)
        {
            return this.context.Requests.Where(x => x.UID == requestId).FirstOrDefault();
        }

        public void AddFriend(ApplicationUser currentUser, FriendRequest fr)
        {
            ApplicationUser temp = this.GetUserByEmail(fr.Creator);
            currentUser.Friends.Add(temp);

            this.context.SaveChanges();
        }

        public void RequestRemove(FriendRequest fr)
        {
            this.context.Requests.Remove(fr);
            this.context.SaveChanges();
        }

        public void AddPicture(Picture pic,ApplicationUser currentUser)
        {
            currentUser.Pictures.Add(pic);
            this.context.SaveChanges();
        }

        public List<ApplicationUser> GetUsers(ApplicationUser currentUser)
        {
            return this.context.ApplicationUsers.Where(x => x.Id != currentUser.Id).ToList();
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            return this.context.ApplicationUsers.Where(x => x.Email == email).FirstOrDefault();
        }

        public Picture GetPictureByUID(string uid)
        {
            return this.context.Pictures.Where(x => x.UID == uid).FirstOrDefault();
        }

        public void AddLike(Picture pic, ApplicationUser currentUser)
        {
            pic.Persons.Add(new Like() { UID = Guid.NewGuid().ToString(), Creator = currentUser });
            pic.Likes = pic.Persons.Count;
            this.context.SaveChanges();
        }

        public void DeleteLike(Picture pic, ApplicationUser currentUser)
        {
            Like l = this.context.Likes.Where(x => x.Creator.Email == currentUser.Email).FirstOrDefault();
            pic.Persons.Remove(l);
            pic.Likes = pic.Persons.Count;
            this.context.SaveChanges();
        }
    }
}
