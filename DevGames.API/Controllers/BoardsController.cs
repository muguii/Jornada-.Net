using AutoMapper;
using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevGames.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly DevGamesContext context;
        private readonly IMapper mapper;

        public BoardsController(DevGamesContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(context.Boards);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Board board = context.Boards.SingleOrDefault(board => board.Id == id);

            if (board == null)
            {
                return NotFound(); 
            }

            return base.Ok(board);
        }

        [HttpPost]
        public IActionResult Post(AddBoardInputModel model/*, [FromServices] IMapper mapperFromMethod*/)
        {
            //Board board = new Board(model.Id, model.GameTitle, model.Description, model.Rules);
            Board board = mapper.Map<Board>(model);

            context.Boards.Add(board);

            return CreatedAtAction("GetById", new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateBoardInputModel model)
        {
            Board board = context.Boards.SingleOrDefault(board => board.Id == id);

            if (board == null)
            {
                return NotFound();
            }

            board.Update(model.Description, model.Rules);

            return NoContent();
        }
    }
}
