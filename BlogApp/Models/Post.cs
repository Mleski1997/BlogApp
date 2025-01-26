﻿using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class Post
    {
        public int Id { get; set; }


        public string Title { get; set; }

        public string  Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
