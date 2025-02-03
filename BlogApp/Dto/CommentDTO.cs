using BlogApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.DTO
{
    public class CommentDTO
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string AuthorName { get; set; } = "Anonymus";
        [Required]
        [StringLength(1000)]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid PostId { get; set; }
        public Post Post { get; set; }

    }
}
