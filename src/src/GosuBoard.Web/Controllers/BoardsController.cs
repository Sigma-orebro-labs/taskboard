using GosuBoard.Web.Controllers.Results;
using GosuBoard.Web.Entities;
using GosuBoard.Web.Infrastructure;
using GosuBoard.Web.Models;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace GosuBoard.Web.Controllers
{
    [Route("api/boards")]
    public class BoardsController : BaseController
    {
        [HttpGet(Name = "Boards")]
        public CollectionModel<BoardModel> Get()
        {
            using (var context = new BoardContext())
            {
                var href = Url.Link("Boards", null);
                return Result.Collection(href, context.Boards);
            }
        }

        [HttpGet("{id}", Name = "BoardById")]
        public IActionResult Get(int id)
        {
            using (var context = new BoardContext())
            {
                var board = context.Boards.FirstOrDefault(x => x.Id == id);

                if (board == null)
                    return HttpNotFound();

                return Result.Object(board);
            }
        }

        [HttpPost]
        public IActionResult Post(string name)
        {
            var board = new Board
            {
                Name = name
            };

            using (var context = new BoardContext())
            {
                context.Boards.Add(board);

                context.SaveChanges();
            }

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