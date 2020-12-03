﻿using Backend.Data;
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
            ApplicationUser friend = this.context.ApplicationUsers.Where(x => x.Email == "almasipatrik3@gmail.com").FirstOrDefault();
            ApplicationUser currentUserv2 = this.context.ApplicationUsers.Where(x => x.Email == "almasipatrik@gmail.com").FirstOrDefault();
            currentUserv2.FirstName = "TESZT";
            if (friend.Id != null)
            {
                //Add the request in his/her friend request list.
                FriendRequest newRequest = new FriendRequest()
                {
                    Creator = currentUserv2.Email,
                    Time = DateTime.Now,
                    UID = Guid.NewGuid().ToString()
                };

                friend.Requests.Add(newRequest);

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
    }
}
