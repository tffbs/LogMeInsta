using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Hubs;
using Backend.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext context;
        private readonly IHubContext<ChatHub> hub;

        public MessageController(UserManager<IdentityUser> userManager, IConfiguration configuration, ApplicationDbContext context, IHubContext<ChatHub> hub)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.context = context;
            this.hub = hub;
        }


        [HttpPost]
        [Route("getall")]
        public ActionResult<IEnumerable<Message>> GetAll([FromBody] Message value)
        
        {
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;
            return this.context.Messages.Where( x => (x.Sender == currentUser.Email && x.Receiver == value.Receiver) ||  (x.Sender == value.Receiver && x.Receiver == currentUser.Email) ).OrderBy(x => x.Date).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Message> Get(string id)
        {
            return context.Messages.FirstOrDefault(t => t.UID == id);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Message value)
        {
            ApplicationUser currentUser = (ApplicationUser)userManager.GetUserAsync(this.User).Result;

            value.UID = Guid.NewGuid().ToString();
            value.Date = DateTime.Now;
            value.Sender = currentUser.Email;

            context.Messages.Add(value);
            context.SaveChanges();

            await hub.Clients.All.SendAsync("NewMessage", value);
            return Ok();

        }

        [HttpPut("{id}")]
        public void Put(string id, [FromBody] Message value)
        {
            var old = context.Messages.FirstOrDefault(t => t.UID == id);
            value.UID = id;
            context.Remove(old);
            context.Add(value);
            context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var old = context.Messages.FirstOrDefault(t => t.UID == id);
            context.Remove(old);
            context.SaveChanges();
        }
    }
}