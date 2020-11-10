using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ApplicationDbContext context;
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;

        }

        [Route("addfriend")]
        [HttpPost]
        public IActionResult AddFriend(string email)
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;

            //find his/her friend.
            ApplicationUser friend = this.context.ApplicationUsers.Where(x => x.Email == email).FirstOrDefault();
            if (friend.Id != null)
            {
                //Add the request in his/her friend request list.
                friend.Requests.Add(new FriendRequest()
                {
                    Creator = friend,
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
        [Route("friends")]
        [HttpGet]
        public IActionResult ListFriends()
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;
            if (currentUser.Id != null)
                return Ok(currentUser.Friends);
            else
                return BadRequest();
        }

        //[Authorize]
        //[Route("logout")]
        //public async Task<IActionResult> LogOut(string returnUrl = null)
        //{
        //}

        [Route("requests")]
        [HttpGet]
        public JsonResult GetFriendRequests()
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;

            return new JsonResult(currentUser.Requests);
        }

        [Route("feed")]
        [HttpGet]
        public IActionResult Feed()
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;
            if (currentUser.Id != null)
                return Ok(currentUser.Friends.Select(x => x.Pictures).ToList());
            else
                return BadRequest();
        }

        [Route("profile")]
        [HttpGet]
        public IActionResult OwnPictures()
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;
            if (currentUser.Id != null)
                return Ok(currentUser.Pictures);
            else
                return BadRequest();
        }

        [Route("acceptorreject")]
        [HttpPost]
        public IActionResult AcceptOrReject(string requestId, bool accepted)
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;
            FriendRequest request = currentUser.Requests.Where(x => x.UID == requestId).FirstOrDefault();
            if (request.UID == null)
                return BadRequest();

            if (accepted)
            {
                currentUser.Friends.Add(request.Creator);
                currentUser.Requests.Remove(request);
            }
            else
                currentUser.Requests.Remove(request);

            this.context.SaveChanges();
            return Ok();

        }
    }
}
