using System.Threading.Tasks;
using ITunes.UrlShortener.Entities.RequestModel.Admin;
using ITunes.UrlShortener.Entities.ResponseModel;
using ITunes.UrlShortener.Entities.ResponseModel.Admin.Login;

namespace ITunes.UrlShortener.Services.Abstraction
{
    public interface IAuthenticationServiceUnit
    {
        Task<ApplicationResponse<bool>> Logout();
        
        Task<ApplicationResponse<LoginResponseDto>> Authenticate(LoginRequestDto authenticateRequest);
    }
}