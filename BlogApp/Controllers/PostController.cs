
using BlogApp.DTO;
using BlogApp.Interfaces;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
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

        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postService.GetPostAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPost([FromBody] PostDTO postDTO)
        {
          await _postService.AddPostAsync(postDTO);
          return CreatedAtAction(nameof(GetPost), new {id = postDTO.Id}, value:null);
        }

        [HttpDelete("delete")]

        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.DeletePostAsync(id);
            return Ok();
        }
    }
}
