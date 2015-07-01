using TaskBoard.Web.Controllers.Results;
using TaskBoard.Web.Entities;
using TaskBoard.Web.Infrastructure;
using TaskBoard.Web.Models;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace TaskBoard.Web.Controllers
{
    [Route("api/boards")]
    public class BoardsController : BaseController
    {
        private BoardContext _context;

        public BoardsController(BoardContext boardContext)
            : base(boardContext)
        {
            _context = boardContext;
        }

        [HttpGet(Name = "Boards")]
        public CollectionModel<BoardModel> Get()
        {
            var href = Url.Link("Boards", null);
            return Result.Collection(href, _context.Boards);
        }

        [HttpGet("{id}", Name = "BoardById")]
        public IActionResult Get(int id)
        {
            var board = _context.Boards.FirstOrDefault(x => x.Id == id);

            if (board == null)
                return HttpNotFound();

            return Result.Object(board);
        }

        [HttpPost]
        public IActionResult Post(string name)
        {
            var board = new Board
            {
                Name = name
            };

            _context.Boards.Add(board);
            _context.SaveChanges();

            return Result.Created(board);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string name)
        {
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return DeleteById<Board>(id);
        }

        private BoardModelResultFactory Result
        {
            get { return new BoardModelResultFactory(Url); }
        }
    }
}