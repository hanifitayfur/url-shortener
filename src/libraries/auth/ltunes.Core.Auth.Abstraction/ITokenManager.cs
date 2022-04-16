using System.Threading.Tasks;

namespace ltunes.Core.Auth.Abstraction
{
    public interface ITokenManager
    {
        Task<bool> AssignToken(string token,RefreshToken refreshToken);
        Task<bool> RemoveToken(string token);
    }
}