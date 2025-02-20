
using BlogApp.DTO;
using BlogApp.Interfaces;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlogApp.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {

        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            if (posts == null)
            {
                return NotFound();
            }
            return Ok(posts);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetPost(Guid id)
        {
            var post = await _postService.GetPostAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody] CreatePostDTO createPostDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdPost = await _postService.AddPostAsync(createPostDTO);

            var post = await _postService.GetPostAsync(createdPost.Id);

            return CreatedAtAction(nameof(GetPost), new {id = post.Id}, value:post);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeletePost(Guid id)
        {
            await _postService.DeletePostAsync(id);
            return NoContent();
        }
        [HttpPut]

        public async Task<IActionResult> UpdatePost(Guid Id, [FromBody] UpdatePostDTO updatePostDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatePost = await _postService.UpdatePostAsync(Id, updatePostDTO);
            return Ok(updatePost);
        }
    }
}
