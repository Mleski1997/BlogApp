using System.ComponentModel.DataAnnotations;

namespace BlogApp.DTO
{
    public class AddReplyDTO
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string AuthorName { get; set; }

        [Required] 
        public Guid ParentCommentId { get; set; }
    }
}
