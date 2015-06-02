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

                return new ObjectResult(ToModel(issue));
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
        public IActionResult Post(int boardId, string title)
        {
            var issue = new Issue
            {
                Title = title,
                BoardId = boardId
            };

            using (var context = new BoardContext())
            {
                context.Issues.Add(issue);

                context.SaveChanges();
            }
            
            return Created(ToModel(issue));
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

                var models = issues.Select(ToModel);

                return new CollectionModel<IssueModel>(models, href);
            }
        }

        private LinkModel CreateBoardLink(int boardId)
        {
            var href = Url.Link("BoardById", new { id = boardId });
            return new LinkModel("board", "Board", href);
        }

        private LinkModel CreateSelfLink(int id)
        {
            var href = Url.Link("IssueById", new { id = id });
            return new LinkModel("self", "Self", href);
        }

        private IssueModel ToModel(Issue issue)
        {
            var issueModel = issue.ToModel();

            issueModel.AddLink(CreateBoardLink(issue.BoardId));
            issueModel.AddLink(CreateSelfLink(issue.Id));

            return issueModel;
        }
    }
}
