using System.Threading.Tasks;
using ITunes.UrlShortener.Entities.Entities.MongoDB;
using ITunes.UrlShortener.Entities.RequestModel.User;
using ITunes.UrlShortener.Entities.ResponseModel;

namespace ITunes.UrlShortener.Services.Abstraction
{
    public interface IUserService
    {
        Task<ApplicationResponse<bool>> AddUser(AddUserDto addUserDto);

        Task<User> GetUserById(string userId);
    }
}