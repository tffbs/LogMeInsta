using Backend.Data;
using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Backend.Repositories
{
    public class UserRepository
    {
        ApplicationDbContext context;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            this.context = applicationDbContext;
        }

        public bool AddFriendRequest(string email, ApplicationUser currentUser)
        {
            ApplicationUser friend = this.context.ApplicationUsers.Where(x => x.Email == email).FirstOrDefault();
            if (friend.Id != null)
            {
                //Add the request in his/her friend request list.
                friend.Requests.Add(new FriendRequest()
                {
                    Creator = currentUser,
                    Time = DateTime.Now,
                    UID = Guid.NewGuid().ToString()
                });
                this.context.SaveChanges();
                return true;
            }
            return false;
        }

        public FriendRequest GetUserRequest(string requestId)
        {
            return this.context.Requests.Where(x => x.UID == requestId).FirstOrDefault();
        }

        public void AddFriend(ApplicationUser currentUser, FriendRequest fr)
        {
            currentUser.Friends.Add(fr.Creator);

            this.context.SaveChanges();
        }

        public void RequestRemove(FriendRequest fr)
        {
            this.context.Requests.Remove(fr);
            this.context.SaveChanges();
        }

        public void AddPicture(string picture, ApplicationUser currentUser)
        {
            currentUser.Pictures.Add(new Picture()
            {
                UID = Guid.NewGuid().ToString(),
                User = currentUser,
                Likes = 0,
                PictureData = picture
            });

            this.context.SaveChanges();
        }
    }
}
