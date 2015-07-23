using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskBoard.Domain.Entities;
using TaskBoard.Persistence;
using TaskBoard.Web.Controllers.RealTime;
using TaskBoard.Web.Controllers.Results;
using TaskBoard.Web.Models;
using System.Linq;
using System.Collections.Generic;

namespace TaskBoard.Web.Controllers
{
    public class IssueStatesController : BaseController
    {
        public IHttpActionResult Get(int id)
        {
            var state = Context.IssueStates.FirstOrDefault(x => x.Id == id);

            if (state == null)
                return NotFound();

            return Result.Object(state);
        }

        // GET api/values/5
        [Route("~/api/boards/{boardId}/states", Name = "StatesByBoardId")]
        public CollectionModel<IssueStateModel> GetByBoardId(int boardId)
        {
            var issueStates = Context.IssueStates.Where(x => x.BoardId == boardId);

            return Collection(boardId, issueStates);
        }

        [Route("~/api/boards/{boardId}/states")]
        public IHttpActionResult Post(int boardId, IssueStateModel postedModel)
        {
            var previousMaxOrderValue = Context.IssueStates
                .Where(x => x.BoardId == boardId)
                .Max(x => x.Order);

            var state = new IssueState
            {
                Name = postedModel.Name,
                BoardId = boardId,
                Order = previousMaxOrderValue + 1
            };

            Context.IssueStates.Add(state);
            Context.SaveChanges();

            ConnectionManager.BroadcastAddState(Result.ToModel(state));

            return Result.Created(state);
        }

        public HttpResponseMessage Put(IssueStateModel model)
        {
            IList<IssueState> allStatesForBoard = null;

            // Make sure that the id and board id match an existing state
            var stateToUpdate = Context.IssueStates.FirstOrDefault(x => x.Id == model.Id && x.BoardId == model.BoardId);

            if (stateToUpdate == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            if (model.Order != stateToUpdate.Order)
            {
                allStatesForBoard = UpdateOrder(model, stateToUpdate, Context);
            }

            stateToUpdate.Name = model.Name;

            Context.SaveChanges();

            var wasOrderUpdated = allStatesForBoard != null;

            if (wasOrderUpdated)
                ConnectionManager.BroadcastUpdateStates(stateToUpdate.BoardId, Collection(stateToUpdate.BoardId, allStatesForBoard));
            else
                ConnectionManager.BroadcastUpdateState(Result.ToModel(stateToUpdate));

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        private IList<IssueState> UpdateOrder(IssueStateModel updatedModel, IssueState stateToUpdate, BoardContext context)
        {
            var allStatesForBoard = context.IssueStates.Where(x => x.BoardId == stateToUpdate.BoardId).ToList();
            var otherStates = allStatesForBoard.Where(x => x.Id != stateToUpdate.Id)
                .OrderBy(x => x.Order)
                .ToList();

            var oldOrder = stateToUpdate.Order;
            var newOrder = updatedModel.Order;

            if (newOrder < 0)
                newOrder = 0;
            if (newOrder >= allStatesForBoard.Count)
                newOrder = allStatesForBoard.Count - 1;

            for (int i = 0; i < otherStates.Count; i++)
            {
                otherStates[i].Order = i;
            }

            foreach (var state in otherStates.Where(x => x.Order >= newOrder))
            {
                state.Order += 1;
            }
            
            stateToUpdate.Order = newOrder;

            return allStatesForBoard;
        }

        public HttpResponseMessage Delete(int id)
        {
            return DeleteById<IssueState>(id, x =>
            {
                var issueStateModel = Result.ToModel(x);
                ConnectionManager.BroadcastDeleteState(issueStateModel);
            });
        }
        
        private IssueStateModelResultFactory Result
        {
            get { return new IssueStateModelResultFactory(Url, this); }
        }

        private CollectionModel<IssueStateModel> Collection(int boardId, IEnumerable<IssueState> issueStates)
        {
            var href = Url.Link("StatesByBoardId", new { boardId });
            return Result.Collection(href, issueStates);
        }
    }
}
