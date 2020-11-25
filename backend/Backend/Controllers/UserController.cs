using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Backend.Data;
using Backend.Model;
using Backend.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        UserRepository userRepository;

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userRepository = new UserRepository(context);
        }

        [Route("addfriend")]
        public IActionResult AddFriend(string email)
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;

            if (userRepository.AddFriendRequest(email, currentUser))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [Route("friends")]
        public IActionResult ListFriends()
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;
            if (currentUser.Id != null)
                return Ok(currentUser.Friends.Select(x => new
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email
                }));
            else
                return BadRequest();
        }

        //[Authorize]
        //[Route("logout")]
        //public async Task<IActionResult> LogOut(string returnUrl = null)
        //{
        //}

        [Route("requests")]
        public IActionResult GetFriendRequests()
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;
            return Ok(currentUser.Requests.Select(x =>
            new
            {
                x.Creator.LastName,
                x.Creator.FirstName,
                x.Creator.Email
            }).ToList());
        }

        [Route("feed")]
        public IActionResult Feed()
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;
            if (currentUser.Id != null)
                return Ok(currentUser.Friends.Select(x => new
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Pictures = x.Pictures.Select(y => new
                    {
                        Likes = y.Likes,
                        Picture = y.PictureData,
                        Uid = y.UID
                    })
                }));
            else
                return BadRequest();
        }

        [Route("profile")]
        public IActionResult OwnPictures()
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;
            if (currentUser.Id != null)
                return Ok(currentUser.Pictures.Select(x => new
                {
                    Likes = x.Likes,
                    Picture = x.PictureData,
                    Uid = x.UID
                }));
            else
                return BadRequest();
        }

        [Route("acceptorreject")]
        public IActionResult AcceptOrReject(string requestId, bool accepted)
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;
            FriendRequest request = userRepository.GetUserRequest(requestId);
            if (request.UID == null)
                return BadRequest();

            if (accepted)
            {
                userRepository.AddFriend(currentUser, request);
                userRepository.RequestRemove(request);
            }
            else
            {
                userRepository.RequestRemove(request);
            }

            return Ok();
        }

        [Route("addpicture")]
        public IActionResult AddPicture(string picture)
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;
            if (currentUser.Id != null)
            {
                userRepository.AddPicture(picture, currentUser);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
