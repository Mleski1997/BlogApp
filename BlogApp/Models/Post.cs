using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class Post
    {
        public Guid Id { get; set; } = Guid.NewGuid();

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
