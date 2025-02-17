using BlogApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.DTO
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public string AuthorName { get; set; } = "Anonymus";
        [Required]
        [StringLength(1000)]
        public string Content { get; set; }     
      
        [Required]
        public Guid PostId { get; set; }
        public Guid? ParentCommentId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    }
}
