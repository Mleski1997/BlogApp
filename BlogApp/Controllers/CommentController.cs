using BlogApp.DTO;
using BlogApp.Interfaces;
using BlogApp.Models;
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
        
            return Ok(comments);
        }

        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetPostComments(Guid postId)
        {
            var comments = await _commentService.GetPostCommentsAsync(postId);
            
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(Guid id)
        {
            var comment = await _commentService.GetCommentAsync(id);
           
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CreateCommentDTO createCommentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var createdComment = await _commentService.AddCommentAsync(createCommentDTO);

            return CreatedAtAction(nameof(GetComment), new { id = createdComment.Id }, createdComment);
        }

        [HttpPost("{parentCommentId}/reply")]
        public async Task<IActionResult> AddReply(Guid parentCommentId, [FromBody] CreateCommentDTO createCommentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parentComment = await _commentService.GetCommentAsync(parentCommentId);
            if (parentComment == null)
            {
                return NotFound($"Parent comment with id {parentCommentId} not found.");
            }

            createCommentDTO.ParentCommentId = parentCommentId;
            createCommentDTO.PostId = parentComment.PostId;

            var createdReply = await _commentService.AddCommentAsync(createCommentDTO);
            return CreatedAtAction(nameof(GetComment), new { id = createdReply.Id }, createdReply);
        }
        

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            await _commentService.DeleteCommentAsync(id);
            return NoContent();
        }
    }
}
    