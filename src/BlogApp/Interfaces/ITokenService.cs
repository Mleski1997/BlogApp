using BlogApp.Models;

namespace BlogApp.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
