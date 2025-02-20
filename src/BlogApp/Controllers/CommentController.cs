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
        public async Task<IActionResult> AddComment(Guid postId, [FromBody] CreateCommentDTO createCommentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdComment = await _commentService.AddCommentAsync(postId, createCommentDTO);

            return CreatedAtAction(nameof(GetComment), new { id = createdComment.Id }, createdComment);
        }

        [HttpPost("{parentCommentId}/reply")]
        public async Task<IActionResult> AddReply(Guid postId, Guid parentCommentId,[FromBody] CreateCommentDTO createCommentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdReply = await _commentService.AddReplyAsync(postId, parentCommentId, createCommentDTO);
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
    