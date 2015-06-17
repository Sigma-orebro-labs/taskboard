using TaskBoard.Web.Controllers.Results;
using TaskBoard.Web.Entities;
using TaskBoard.Web.Infrastructure;
using TaskBoard.Web.Mapping;
using TaskBoard.Web.Models;
using Microsoft.AspNet.Mvc;
using System.Linq;
using Microsoft.AspNet.SignalR.Infrastructure;
using TaskBoard.Web.Controllers.RealTime;

namespace TaskBoard.Web.Controllers
{
    [Route("api/issuestates")]
    public class IssueStatesController : BaseController
    {
        private IConnectionManager _connectionManager;

        public IssueStatesController(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        [HttpGet("{id}", Name = "StateById")]
        public IActionResult Get(int id)
        {
            using (var context = new BoardContext())
            {
                var state = context.IssueStates.FirstOrDefault(x => x.Id == id);

                if (state == null)
                    return HttpNotFound();

                return Result.Object(state);
            }
        }

        // GET api/values/5
        [HttpGet("~/api/boards/{boardId}/states", Name = "StatesByBoardId")]
        public CollectionModel<IssueStateModel> GetByBoardId(int boardId)
        {
            var href = Url.Link("StatesByBoardId", new { boardId });

            using (var context = new BoardContext())
            {
                var issueStates = context.IssueStates.Where(x => x.BoardId == boardId);

                return Result.Collection(href, issueStates);
            }
        }

        [HttpPost("~/api/boards/{boardId}/states")]
        public IActionResult Post(int boardId, string name)
        {
            var state = new IssueState
            {
                Name = name,
                BoardId = boardId
            };

            using (var context = new BoardContext())
            {
                context.IssueStates.Add(state);

                context.SaveChanges();
            }

            _connectionManager.BroadcastAddState(Result.ToModel(state));

            return Result.Created(state);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return DeleteById<IssueState>(id, x =>
            {
                var issueStateModel = Result.ToModel(x);
                _connectionManager.BroadcastDeleteState(issueStateModel);
            });
        }
        
        private IssueStateModelResultFactory Result
        {
            get { return new IssueStateModelResultFactory(Url); }
        }
    }
}
