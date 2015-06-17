using TaskBoard.Web.Controllers.Results;
using TaskBoard.Web.Entities;
using TaskBoard.Web.Infrastructure;
using TaskBoard.Web.Mapping;
using TaskBoard.Web.Models;
using Microsoft.AspNet.Mvc;
using System.Linq;
using Microsoft.AspNet.SignalR.Infrastructure;
using TaskBoard.Web.Controllers.RealTime;
using System;
using System.Collections.Generic;

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
            using (var context = new BoardContext())
            {
                var issueStates = context.IssueStates.Where(x => x.BoardId == boardId);

                return Collection(boardId, issueStates);
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
        public class Foo
        {
            public int Id { get; set; }
            public int BoardId { get; set; }
        }
        [HttpPut("{id}")]
        public IActionResult Put(IssueStateModel model)
        {
            using (var context = new BoardContext())
            {
                IList<IssueState> allStatesForBoard = null;

                // Make sure that the id and board id match an existing state
                var stateToUpdate = context.IssueStates.FirstOrDefault(x => x.Id == model.Id && x.BoardId == model.BoardId);

                if (stateToUpdate == null)
                    return new HttpNotFoundResult();

                if (model.Order != stateToUpdate.Order)
                {
                    allStatesForBoard = UpdateOrder(model, stateToUpdate, context);
                }

                stateToUpdate.Name = model.Name;

                context.SaveChanges();

                var wasOrderUpdated = allStatesForBoard != null;

                if (wasOrderUpdated)
                    _connectionManager.BroadcastUpdateStates(stateToUpdate.BoardId, Collection(stateToUpdate.BoardId, allStatesForBoard));
                else
                    _connectionManager.BroadcastUpdateState(Result.ToModel(stateToUpdate));
            }

            return new NoContentResult();
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

        private CollectionModel<IssueStateModel> Collection(int boardId, IEnumerable<IssueState> issueStates)
        {
            var href = Url.Link("StatesByBoardId", new { boardId });
            return Result.Collection(href, issueStates);
        }
    }
}
