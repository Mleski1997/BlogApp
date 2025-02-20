using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string AuthorName { get; set; } = "Anonymus";
        [Required]
        [StringLength(1000)]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public Guid? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }
        public List<Comment> Replies { get; set; } = new List<Comment>();

    }
}
