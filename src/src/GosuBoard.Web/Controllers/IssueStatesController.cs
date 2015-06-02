using GosuBoard.Web.Entities;
using GosuBoard.Web.Infrastructure;
using GosuBoard.Web.Mapping;
using GosuBoard.Web.Models;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace GosuBoard.Web.Controllers
{
    [Route("api/[controller]", Name = "StatesController")]
    public class IssueStatesController : BaseController
    {
        [HttpGet("{id}", Name = "StateById")]
        public IActionResult Get(int id)
        {
            using (var context = new BoardContext())
            {
                var state = context.IssueStates.FirstOrDefault(x => x.Id == id);

                if (state == null)
                    return HttpNotFound();

                return new ObjectResult(ToModel(state));
            }
        }

        // GET api/values/5
        [Route("~/api/Boards/{boardId}/States", Name = "StatesByBoardId")]
        [HttpGet("{boardId}")]
        public CollectionModel<IssueStateModel> GetByBoardId(int boardId)
        {
            var href = Url.Link("StatesByBoardId", new { boardId });

            using (var context = new BoardContext())
            {
                var models = context.IssueStates.Where(x => x.BoardId == boardId).Select(ToModel);

                return new CollectionModel<IssueStateModel>(models, href);
            }
        }

        [HttpPost("~/api/Boards/{boardId}/States")]
        public IActionResult Post(int boardId, string name)
        {
            var State = new IssueState
            {
                Name = name,
                BoardId = boardId
            };

            using (var context = new BoardContext())
            {
                context.IssueStates.Add(State);

                context.SaveChanges();
            }

            return Created(ToModel(State));
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return DeleteById<IssueState>(id);
        }

        private LinkModel CreateBoardLink(int boardId)
        {
            var href = Url.Link("BoardById", new { id = boardId });
            return new LinkModel("board", "Board", href);
        }

        private LinkModel CreateSelfLink(int id)
        {
            var href = Url.Link("StateById", new { id = id });
            return new LinkModel("self", "Self", href);
        }

        private IssueStateModel ToModel(IssueState State)
        {
            var model = State.ToModel();

            model.Links.Add(CreateBoardLink(State.BoardId));
            model.Links.Add(CreateSelfLink(State.Id));

            return model;
        }
    }
}
