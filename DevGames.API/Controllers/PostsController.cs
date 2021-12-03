using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevGames.API.Controllers
{
    [Route("api/boards/{id}/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly DevGamesContext context;

        public PostsController(DevGamesContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAll(int id)
        {
            Board board = context.Boards.SingleOrDefault(board => board.Id == id);

            if (board == null)
            {
                return NotFound();  
            }

            return Ok(board.Posts);
        }

        [HttpGet("{postId}")]
        public IActionResult GetById(int id, int postId)
        {
            Board board = context.Boards.SingleOrDefault(board => board.Id == id);

            if (board == null)
            {
                return NotFound();
            }

            Post post = board.Posts.SingleOrDefault(post => post.Id == postId);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPost]
        public IActionResult Post(int id, AddPostInputModel model)
        {
            Board board = context.Boards.SingleOrDefault(board => board.Id == id);

            if (board == null)
            {
                return NotFound();
            }

            Post post = new Post(model.Id, model.Title, model.Description);

            board.Posts.Add(post);

            return CreatedAtAction(nameof(GetById), new { id = id, postId = model.Id }, model);
        }

        [HttpPut("{postId}/comments")]
        public IActionResult PostComment(int id, int postId, AddCommentInputModel model)
        {
            Board board = context.Boards.SingleOrDefault(board => board.Id == id);

            if (board == null)
            {
                return NotFound();
            }

            Post post = board.Posts.SingleOrDefault(post => post.Id == postId);

            if (post == null)
            {
                return NotFound();
            }

            Comment comment = new Comment(model.Title, model.Description, model.User);

            post.AddComment(comment);

            return NoContent();
        }
    }
}
