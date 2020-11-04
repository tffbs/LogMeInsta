using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ApplicationDbContext context;
        UserManager<IdentityUser> userManager;

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [Route("addfriend")]
        public IActionResult AddFriend(string id)
        {
            //find currentUser
            var myself = this.User;
            var userObj = (ApplicationUser)userManager.GetUserAsync(myself).Result;

            //find his/her friend.
            ApplicationUser friend = this.context.ApplicationUsers.Where(x => x.Id == id).FirstOrDefault();
            if (friend.Id != null)
            {
                //Add the request in his/her friend request list.
                friend.Requests.Add(new FriendRequest()
                {
                    Creator = userObj,
                    Time = DateTime.Now,
                    UID = Guid.NewGuid().ToString()
                });
                this.context.SaveChanges();
                return Ok();
            }
            else
                return BadRequest();

        }

        [Authorize]
        public IActionResult ListFriends()
        {
            //find currentUser
            var myself = this.User;
            var userObj = (ApplicationUser)userManager.GetUserAsync(myself).Result;

            if (userObj.Id != null)
                return Ok(userObj.Friends);
            else
                return BadRequest();
        }

        public JsonResult GetFriendRequests()
        {
            //find currentUser
            var myself = this.User;
            var userObj = (ApplicationUser)userManager.GetUserAsync(myself).Result;

            return new JsonResult(userObj.Requests);
        }

        public IActionResult Feed()
        {
            //find currentUser
            var myself = this.User;
            var userObj = (ApplicationUser)userManager.GetUserAsync(myself).Result;
            if (userObj.Id != null)
                return Ok(userObj.Friends.Select(x => x.Pictures).ToList());
            else
                return BadRequest();
        }

        public IActionResult OwnPictures()
        {
            //find currentUser
            var myself = this.User;
            var userObj = (ApplicationUser)userManager.GetUserAsync(myself).Result;

            if (userObj.Id != null)
                return Ok(userObj.Pictures);
            else
                return BadRequest();
        }

        public IActionResult AcceptOrReject(string requestId, bool accepted)
        {
            //find currentUser
            var myself = this.User;
            var userObj = (ApplicationUser)userManager.GetUserAsync(myself).Result;
            FriendRequest request = userObj.Requests.Where(x => x.UID == requestId).FirstOrDefault();
            if (request.UID == null)
                return BadRequest();

            if (accepted)
            {
                userObj.Friends.Add(request.Creator);
                userObj.Requests.Remove(request);
            }
            else
                userObj.Requests.Remove(request);

            this.context.SaveChanges();
            return Ok();

        }
    }
}
