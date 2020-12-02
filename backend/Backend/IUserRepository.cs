using Backend.Model;

namespace Backend
{
    public interface IUserRepository
    {
        void AddPicture(Picture newPic,ApplicationUser currentUser);
    }
}