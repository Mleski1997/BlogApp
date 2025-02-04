using BlogApp.DTO;
using BlogApp.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AccountService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        
        

            
        
    }
}
