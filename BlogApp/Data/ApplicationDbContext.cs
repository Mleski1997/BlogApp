using BlogApp.Exceptions;
using BlogApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //var adminId = "A1B2C3D4-E5F6-1234-5678-90ABCDEF1234";
            //var roleId = "B1C2D3E4-F5G6-2345-6789-0ABCDEF56789";


            //var adminRole = new IdentityRole
            //{
            //    Id = roleId,
            //    Name = "Admin",
            //    NormalizedName = "ADMIN"
            //};

            //modelBuilder.Entity<IdentityRole>().HasData(adminRole);

            //var adminUser = new AppUser
            //{
            //    Id = adminId,
            //    UserName = "Admin",
            //    NormalizedUserName = "ADMIN",
            //    Email = "admin@blogapi.com",
            //    NormalizedEmail = "ADMIN@BLOGAPI.com",
            //    EmailConfirmed = true,
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //    ConcurrencyStamp = Guid.NewGuid().ToString(),
            //};

            //var hasher = new PasswordHasher<AppUser>();
            //adminUser.PasswordHash = hasher.HashPassword(adminUser, "adminPassword123.");

            //modelBuilder.Entity<AppUser>().HasData(adminUser);

            //modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            //{
            //    UserId = adminId,
            //    RoleId = roleId
            //});


            modelBuilder.Entity<Post>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.Post)
                .HasForeignKey(e => e.PostId)
                .IsRequired(false);

        }
       

    }
   
}
