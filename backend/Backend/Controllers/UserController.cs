using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
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

        [Route("users")]
        [HttpGet]
        public string Users()
        {
            string connectionId = GetConnectionId();
            List<UserViewModel> users = new List<UserViewModel>();
            for (int i = 0; i < 5; i++)
                users.Add(GetUserViewModel());

            return JsonConvert.SerializeObject(users);
        }

        //Pure function, database missing
        private UserViewModel GetUserViewModel()
        {
            Random r = new Random();

            //Generate random user
            string fname = string.Empty;
            string lname = string.Empty;
            for (int i = 0; i < 5; i++)
            {
                fname += (char)r.Next(65, 91);
                lname += (char)r.Next(65, 91);
            }

            return new UserViewModel(fname,lname,"email");
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
