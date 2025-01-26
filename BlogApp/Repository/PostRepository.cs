using BlogApp.Data;
using BlogApp.Interfaces;
using BlogApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace BlogApp.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddPostAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(Post post)
        {
             _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistingByTitleAsync(string title)
        {
            return await _context.Posts.AnyAsync(p => p.Title == title);
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
           return await _context.Posts.ToListAsync();
        }

        public async Task<Post> GetPostAsync(int id)
        {
            return await _context.Posts.FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
