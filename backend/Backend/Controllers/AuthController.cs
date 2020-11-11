using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> signInManager;
        public AuthController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> signInManager)
        {
            this._userManager = _userManager;
            this.signInManager = signInManager;
        }


        [Route("signin")]
        public IActionResult SignInWithFacebook(string ReturnUrl)
        {
            var redirectUrl = Url.Action(nameof(AuthController.SignInCallback), "auth", new { returnUrl = ReturnUrl });
            return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, FacebookDefaults.AuthenticationScheme);
        }

        [Route("callback")]
        [HttpGet]
        public async Task<IActionResult> SignInCallback(string returnUrl)
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
            //TODO IF AUTHENTICATION IS NOT SUCCESSFUL
            
            var user = await _userManager.FindByEmailAsync(authenticateResult.Ticket.Principal
                .FindFirst(ClaimTypes.Email).Value);
            if(user == null)
            {
                var result = await _userManager.CreateAsync(new ApplicationUser
                {
                    Email = authenticateResult.Ticket.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = authenticateResult.Ticket.Principal.FindFirst(ClaimTypes.Email).Value,
                    FirstName = authenticateResult.Ticket.Principal.FindFirst(ClaimTypes.GivenName).Value,
                    LastName = authenticateResult.Ticket.Principal.FindFirst(ClaimTypes.Surname).Value,
                    Id = authenticateResult.Ticket.Principal.FindFirst(ClaimTypes.NameIdentifier).Value,
                });
                if (!result.Succeeded)
                    throw new System.Exception("User creation failed.");
            }

            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                new ClaimsPrincipal(authenticateResult.Ticket.Principal.Identity));
            return this.LocalRedirect(returnUrl);
        }

        //[HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return this.Redirect("https://localhost:44340/login");
        }
    }
}
