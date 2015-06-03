﻿using GosuBoard.Web.Controllers.Results;
using GosuBoard.Web.Entities;
using GosuBoard.Web.Infrastructure;
using GosuBoard.Web.Mapping;
using GosuBoard.Web.Models;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace GosuBoard.Web.Controllers
{
    [Route("api/issuestates")]
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

            return Result.Created(State);
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
        
        private IssueStateModelResultFactory Result
        {
            get { return new IssueStateModelResultFactory(Url); }
        }
    }
}