﻿using BlogApp.Models;

namespace BlogApp.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
    }
}
