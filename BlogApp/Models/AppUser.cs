using Microsoft.AspNetCore.Identity;

namespace BlogApp.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
