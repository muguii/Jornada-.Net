﻿using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DevGames.API.Controllers
{
    [Route("api/boards/{id}/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository repository;

        public PostsController(IPostRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll(int id)
        {
            return Ok(repository.GetAllByBoard(id));
        }

        [HttpGet("{postId}")]
        public IActionResult GetById(int id, int postId)
        {
            Post post = repository.GetById(postId);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPost]
        public IActionResult Post(int id, AddPostInputModel model)
        {
            Post post = new Post(model.Title, model.Description, id);
            repository.Add(post);

            return CreatedAtAction(nameof(GetById), new { id = post.Id, postId = post.Id }, model);
        }

        [HttpPost("{postId}/comments")]
        public IActionResult PostComment(int id, int postId, AddCommentInputModel model)
        {
            bool postExists = repository.PostExists(postId);

            if (!postExists)
            {
                return NotFound();
            }

            Comment comment = new Comment(model.Title, model.Description, model.User, postId);
            repository.AddComment(comment);

            return NoContent();
        }
    }
}
