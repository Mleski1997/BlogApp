using BlogApp.DTO;
using BlogApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentService.GetAllCommentsAsync();
            if (comments == null)
            {
                return NotFound();
            }

            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(Guid id)
        {
            var comment = await _commentService.GetCommentAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CommentDTO commentDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _commentService.AddCommentAsync(commentDTO);
            var createdComment = await _commentService.GetCommentAsync(commentDTO.Id);
            return CreatedAtAction(nameof(GetComment), new { id = commentDTO.Id}, createdComment);

        }

        [HttpDelete]
        public async Task <IActionResult> DeleteComment(Guid id)
        {   
            await _commentService.DeleteCommentAsync(id);
            return NoContent();
        }
        
    }
}
