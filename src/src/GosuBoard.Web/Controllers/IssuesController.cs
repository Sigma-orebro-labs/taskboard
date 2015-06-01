using GosuBoard.Web.Entities;
using GosuBoard.Web.Infrastructure;
using GosuBoard.Web.Mapping;
using GosuBoard.Web.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;

namespace GosuBoard.Web.Controllers
{
    [Route("api/issues", Name = "IssuesController")]
    public class IssuesController : Controller
    {
        [HttpGet()]
        public CollectionModel<IssueModel> Get()
        {
            var href = Url.Link("IssuesController", null);
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

                var model = issue.ToModel();

                model.Links.Add(CreateBoardLink(issue.BoardId));
                model.Links.Add(CreateSelfLink(issue.Id));

                return new ObjectResult(model);
            }
        }

        private LinkModel CreateBoardLink(int boardId)
        {
            var href = Url.Link("BoardsById", new { id = boardId });
            return new LinkModel("board", "Board", href);
        }

        private LinkModel CreateSelfLink(int id)
        {
            var href = Url.Link("IssueById", new { id = id });
            return new LinkModel("self", "Self", href);
        }

        // GET api/values/5
        [Route("~/api/Boards/{boardId}/Issues", Name = "IssuesByBoardId")]
        [HttpGet("{boardId}")]
        public CollectionModel<IssueModel> GetByBoardId(int boardId)
        {
            var href = Url.Link("IssuesByBoardId", new { boardId });
            return GetCollection(href, x => x.BoardId == boardId);
        }

        [HttpPost("~/api/Boards/{boardId}/Issues")]
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

            var issueModel = issue.ToModel();

            issueModel.Links.Add(CreateBoardLink(issue.BoardId));
            issueModel.Links.Add(CreateSelfLink(issue.Id));

            var newResourceHref = Url.Link("IssueById", new { id = issue.Id });

            return Created(newResourceHref, issueModel);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var context = new BoardContext())
            {
                var issue = context.Issues.FirstOrDefault(x => x.Id == id);

                if (issue == null)
                    return new HttpNotFoundResult();

                context.Issues.Remove(issue);

                context.SaveChanges();
            }

            return new NoContentResult();
        }

        private CollectionModel<IssueModel> GetCollection(string href, Expression<Func<Issue, bool>> predicate = null)
        {
            using (var context = new BoardContext())
            {
                IQueryable<Issue> issues = context.Issues;

                if (predicate != null)
                    issues = issues.Where(predicate);

                var models = issues.ToModels();

                foreach (var model in models)
                {
                    model.Links.Add(CreateBoardLink(model.BoardId));
                    model.Links.Add(CreateSelfLink(model.Id));
                }

                return new CollectionModel<IssueModel>(models, href);
            }
        }
    }
}
