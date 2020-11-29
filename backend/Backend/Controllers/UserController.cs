using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Backend.Data;
using Backend.Model;
using Backend.Repositories;
using Backend.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
namespace Backend.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserManager<IdentityUser> userManager;
        UserRepository userRepository;
        IImageService imageService;

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IImageService imageService)
        {
            this.userManager = userManager;
            this.userRepository = new UserRepository(context);
            this.imageService = imageService;
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

        [Route("friends")]
        public IActionResult ListFriends()
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;
            currentUser.Friends.Add(new ApplicationUser() { Id = Guid.NewGuid().ToString(), FirstName = "TESZT", LastName = "ADAT", Email = "teszt@adat.com" });
            return Ok(currentUser.Friends.Select(x => new
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email
            }));
        }

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
        }

        [Route("profile")]
        public IActionResult OwnPictures()
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;
                return Ok(currentUser.Pictures.Select(x => new
                {
                    Likes = x.Likes,
                    Picture = x.PictureData,
                    Uid = x.UID
                }));
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

        [HttpPost]
        [Route("upload")]
        public async Task AddPicture(IFormFile file)
        {
            var currentUser = await userManager.GetUserAsync(this.User);
            await this.imageService.SaveImageAsync(file, currentUser);
        }

    }
}
