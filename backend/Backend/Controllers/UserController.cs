using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        public ActionResult UserTest()
        {
            return Ok();
        }


        [HttpPost]
        public ActionResult Dashboard(HttpRequestHeaders header)
        {
            var result = new System.Collections.ObjectModel.Collection<string>();
            IEnumerable<string> values;
            if (header.TryGetValues("CONNECTION-ID", out values))
            {
                return Ok();
            }
            return BadRequest();

            HttpHeaders headers = response.Headers;
            IEnumerable<string> values;
            if (headers.TryGetValues("X-BB-SESSION", out values))
            {
                string session = values.First();
            }
        }
    }
}
