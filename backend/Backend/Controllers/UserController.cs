using System;
using System.Collections.Generic;
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

        [Route("like")]
        public IActionResult Like(string uid)
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;
            Picture pic = this.userRepository.GetPictureByUID(uid);
            if (pic.Persons.Any(x => x.Creator.Email == currentUser.Email))
                this.userRepository.DeleteLike(pic,currentUser);
            else
                this.userRepository.AddLike(pic,currentUser);

            return Ok();
        }

        [Route("removefriend")]
        public IActionResult RemoveFriend(string email)
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;

            userRepository.RemoveFriend(email, currentUser);
            return Ok();
        }

        [Route("friends")]
        public IActionResult ListFriends()
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;
            return Ok(currentUser.Friends.Select(x => new
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                ProfilePicture = x.ProfilePic
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
                FirstName = this.userRepository.GetUserByEmail(x.Creator).FirstName,
                LastName = this.userRepository.GetUserByEmail(x.Creator).LastName,
                Email = x.Creator,
                ProfilePicture = this.userRepository.GetUserByEmail(x.Creator).ProfilePic,
                requestId = x.UID
            }).ToList()) ;
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
                    Picture = Convert.ToBase64String(y.PictureData, 0, y.PictureData.Length),
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
                Picture = Convert.ToBase64String(x.PictureData, 0, x.PictureData.Length),
                Uid = x.UID
            }));
        }

        [Route("userinfo")]
        public IActionResult UserInfo()
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;
             return Ok(new
            {
                FirstName = currentUser.LastName,
                LastName = currentUser.FirstName,
                Email = currentUser.Email,
                ProfilePicture = currentUser.ProfilePic,
                NumOffriends = currentUser.Friends.Count,
                Pictures = currentUser.Pictures.Select(y => new
                {
                    Likes = y.Likes,
                    Picture = Convert.ToBase64String(y.PictureData, 0, y.PictureData.Length),
                    Uid = y.UID
                })
            });
        }

        [Route("people")]
        public IActionResult People()
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;
            List<ApplicationUser> users = this.userRepository.GetUsers(currentUser);

            return Ok(users.Select(x=> new
            {
                FirstName = x.LastName,
                LastName = x.FirstName,
                Email = x.Email,
                ProfilePicture = x.ProfilePic,
                isFriend = x.Friends.Any(x => x.Id == currentUser.Id)
            }).ToList());
        }

        [Route("people/{name}")]
        public IActionResult PeopleSearch(string name)
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;
            List<ApplicationUser> users = this.userRepository.GetUsers(currentUser);
            return Ok(users.Select(x => new
            {
                FirstName = x.LastName,
                LastName = x.FirstName,
                Email = x.Email,

            }).Where(x=> (x.FirstName + x.LastName).ToLower().Contains(name.ToLower())));
        }

        [Route("acceptorreject")]
        public IActionResult AcceptOrReject(string requestId, bool accepted)
        {
            //find currentUser
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;
            FriendRequest request = userRepository.GetUserRequest(requestId);
            ApplicationUser friend = this.userRepository.GetUserByEmail(request.Creator);
            if (request.UID == null)
                return BadRequest();

            if (accepted)
            {
                friend.Friends.Add(currentUser);
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
        public async Task<IActionResult> AddPicture(IFormFile file)
        {
            try
            {
                var currentUser = await userManager.GetUserAsync(this.User);
                await this.imageService.SaveImageAsync(file, currentUser);
                return Ok();
            }
            catch (UnauthorizedAccessException e)
            {
                return BadRequest();
            }

        }

    }
}
