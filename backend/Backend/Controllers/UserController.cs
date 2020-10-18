using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Backend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        //Pure function, database missing
        private string GetUser()
        {
            User u = new User()
            {
                UID = "1234$",
                Bio = "",
                FirstName = "Test",
                LastName = "Mock"
            };

            return JsonConvert.SerializeObject(u);
        }


        public string GetConnectionId()
        {
            //check the header
            StringValues headerValues;
            var connectionId = string.Empty;
            if (Request.Headers.TryGetValue("connectionID", out headerValues))
            {
                //validate the token
                connectionId = headerValues.FirstOrDefault();
            }

            //find user by connectionId, serialize and send back as Json.
            return GetUser();
        }
    }
}
