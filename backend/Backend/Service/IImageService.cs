using Backend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Service
{
    public interface IImageService
    {
       bool SaveImageAsync(IFormFile file, IdentityUser currentUser);
    }
}
