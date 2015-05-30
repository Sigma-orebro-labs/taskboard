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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using (var context = new BoardContext())
            {
                var issue = context.Issues.FirstOrDefault(x => x.Id == id);

                if (issue == null)
                    return HttpNotFound();

                var model = issue.ToModel();

                model.BoardLink = CreateBoardLink(issue.BoardId);

                return new ObjectResult(model);
            }
        }

        private LinkModel CreateBoardLink(int boardId)
        {
            var href = Url.Link("BoardsController", new { id = boardId });
            return new LinkModel("board", "Board", href);
        }

        // GET api/values/5
        [Route("~/api/Boards/{boardId}/Issues", Name = "BoardIssues")]
        [HttpGet("{boardId}")]
        public CollectionModel<IssueModel> GetByBoardId(int boardId)
        {
            var href = Url.Link("BoardIssues", new { boardId });
            return GetCollection(href, x => x.BoardId == boardId);
        }

        [HttpPost()]
        public void Post([FromBody]string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
                    model.BoardLink = CreateBoardLink(model.BoardId);
                }

                return new CollectionModel<IssueModel>(models, href);
            }
        }
    }
}
