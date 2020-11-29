using Backend.Model;
using Backend.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Backend.Service
{
    public class ImageService:IImageService
    {
        IUserRepository userRepo;
        ComputerVisionClient visionClient;

        public ImageService(ComputerVisionClient visionClient, IUserRepository userRepo)
        {
            this.visionClient = visionClient;
            this.userRepo = userRepo;
        }

        public async Task SaveImageAsync(IFormFile file, IdentityUser currentUser)
        {
            var bytes = new byte[file.Length];
            using (var stream = file.OpenReadStream())
            {
                await stream.ReadAsync(bytes, 0, bytes.Length);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                var tags = await this.visionClient.TagImageInStreamAsync(stream);
            }
            Picture newPic = new Picture()
            {
                UID = Guid.NewGuid().ToString(),
                Likes = 0,
                PictureData = bytes,
                User = (ApplicationUser)currentUser,
                UserId = currentUser.Id
            };
            userRepo.AddPicture(newPic, (ApplicationUser)currentUser);
        }
    }
}
