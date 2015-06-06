using GosuBoard.Web.Controllers.Results;
using GosuBoard.Web.Entities;
using GosuBoard.Web.Infrastructure;
using GosuBoard.Web.Mapping;
using GosuBoard.Web.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GosuBoard.Web.Controllers
{
    [Route("api/issues")]
    public class IssuesController : BaseController
    {
        [HttpGet(Name = "Issues")]
        public CollectionModel<IssueModel> Get()
        {
            var href = Url.Link("Issues", null);
            return GetCollection(href);
        }

        [HttpGet("{id}", Name = "IssueById")]
        public IActionResult Get(int id)
        {
            using (var context = new BoardContext())
            {
                var issue = context.Issues.FirstOrDefault(x => x.Id == id);

                if (issue == null)
                    return HttpNotFound();

                return Result.Object(issue);
            }
        }

        // GET api/values/5
        [HttpGet("~/api/boards/{boardId}/issues", Name = "IssuesByBoardId")]
        public CollectionModel<IssueModel> GetByBoardId(int boardId)
        {
            var href = Url.Link("IssuesByBoardId", new { boardId });
            return GetCollection(href, x => x.BoardId == boardId);
        }

        [HttpPost("~/api/boards/{boardId}/issues")]
        public IActionResult Post(int boardId, int? stateId, string title)
        {
            var issue = new Issue
            {
                Title = title,
                BoardId = boardId,
                StateId = stateId
            };

            using (var context = new BoardContext())
            {
                context.Issues.Add(issue);

                context.SaveChanges();
            }

            return Result.Created(issue);
        }

        [HttpPost("~/api/boards/{boardId}/issues/{issueId}/statetransitions", Name = "IssueStateTransitions")]
        public IActionResult Post(int boardId, int issueId, int stateId)
        {
            using (var context = new BoardContext())
            {
                var issue = context.Issues.FirstOrDefault(x => x.Id == issueId);

                if (issue == null)
                    return HttpNotFound();

                issue.StateId = stateId;

                context.SaveChanges();
            }

            return new NoContentResult();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return DeleteById<Issue>(id);
        }

        private CollectionModel<IssueModel> GetCollection(string href, Expression<Func<Issue, bool>> predicate = null)
        {
            using (var context = new BoardContext())
            {
                IQueryable<Issue> issues = context.Issues;

                if (predicate != null)
                    issues = issues.Where(predicate);

                return Result.Collection(href, issues);
            }
        }
        private IssueModelResultFactory Result
        {
            get { return new IssueModelResultFactory(Url); }
        }
    }
}
