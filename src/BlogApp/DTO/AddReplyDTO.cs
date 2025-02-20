using System.ComponentModel.DataAnnotations;

namespace BlogApp.DTO
{
    public class AddReplyDTO
    {
        public string AuthorName { get; set; } = "Anonymus";
        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

    }
}
