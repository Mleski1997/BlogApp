using BlogApp.DTO;
using BlogApp.Exceptions;
using BlogApp.Interfaces;
using BlogApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signIn;
        private readonly ITokenService _token;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signIn, ITokenService token)
        {
            _userManager = userManager;
            _signIn = signIn;
            _token = token;
        }

        public async Task<string> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email.ToLower());

            if (user == null)
            {
                throw new InvalidUserException();
            }

            var result = await _signIn.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if (!result.Succeeded)
            {
                throw new InvalidPasswordException();
            };

            return _token.CreateToken(user);
        }
    }
}
