using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Model;
using Microsoft.AspNetCore.Http;
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

        public UserController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [Route("users")]
        [HttpGet]
        public IActionResult Users()
        {
            context.Add(new User() { UID = Guid.NewGuid().ToString(), Email = "asd@xyt.com", Bio = "asdasdasd", FirstName = "jozsi", LastName = "Fred", ProfilePic = "asd", ConnectionID = "asdiujnsdiguhefiuh", });
            this.context.SaveChanges();
            return Ok(this.context.Users);
        }

        [HttpGet]
        private string GetConnectionId()
        {
            //check the header
            StringValues headerValues;
            var connectionId = string.Empty;
            if (Request.Headers.TryGetValue("connectionID", out headerValues))
            {
                //validate the token
                connectionId = headerValues.FirstOrDefault();

                return connectionId;
            }

            throw new Exception("ConnectionID not found in header values.");
        }
    }
}
