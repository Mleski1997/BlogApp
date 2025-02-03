using BlogApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.DTO
{
    public class PostDTO
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title must be shorter than 100 characters.")]
        public string Title { get; set; }
        [Required]
        [MinLength(20, ErrorMessage = "Description must be longer than 20 characters")]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
