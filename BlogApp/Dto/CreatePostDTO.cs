using System.ComponentModel.DataAnnotations;

namespace BlogApp.DTO
{
    public class CreatePostDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "Title must be shorter than 100 characters.")]
        public string Title { get; set; }
        [Required]
        [MinLength(20, ErrorMessage = "Description must be longer than 20 characters")]
        public string Content { get; set; }
    }
}
