using System.Net.Http;
using System.Web.Http;
using TaskBoard.Domain.Entities;
using TaskBoard.Persistence;
using TaskBoard.Web.Controllers.Results;
using TaskBoard.Web.Models;
using System.Linq;

namespace TaskBoard.Web.Controllers
{
    public class BoardsController : BaseController
    {
        public CollectionModel<BoardModel> Get()
        {
            var href = Url.Link("DefaultRouting", new { controller = "boards" });
            return Result.Collection(href, Context.Boards);
        }

        public IHttpActionResult Get(int id)
        {
            var board = Context.Boards.FirstOrDefault(x => x.Id == id);

            if (board == null)
                return NotFound();

            return Result.Object(board);
        }

        public IHttpActionResult Post(BoardModel postedModel)
        {
            var board = new Board
            {
                Name = postedModel.Name
            };

            Context.Boards.Add(board);
            Context.SaveChanges();

            return Result.Created(board);
        }

        public void Put(int id, [FromBody]string name)
        {
        }

        public HttpResponseMessage Delete(int id)
        {
            return DeleteById<Board>(id);
        }

        private BoardModelResultFactory Result
        {
            get { return new BoardModelResultFactory(Url, this); }
        }
    }
}