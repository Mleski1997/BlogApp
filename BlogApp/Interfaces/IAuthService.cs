using BlogApp.DTO;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(LoginDTO loginDTO);
    }
}
