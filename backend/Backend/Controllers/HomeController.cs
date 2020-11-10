//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Backend.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class HomeController : ControllerBase
//    {

//        private readonly IWebHostEnvironment _webHostEnvironment;

//        public HomeController(IWebHostEnvironment webHostEnvironment)
//        {
//            _webHostEnvironment = webHostEnvironment;
//        }

//        [Authorize]
//        public IActionResult AuthorizedFallBack()
//        {
//            var file = _webHostEnvironment.ContentRootFileProvider.GetFileInfo("Clientapp/src/index.html");
//            return PhysicalFile(file.PhysicalPath, "text/html");
//        }
//    }
//}
