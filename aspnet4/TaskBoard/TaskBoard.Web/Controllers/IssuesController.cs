using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskBoard.Domain.Entities;
using TaskBoard.Web.Controllers.RealTime;
using TaskBoard.Web.Controllers.Results;
using TaskBoard.Web.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace TaskBoard.Web.Controllers
{
    public class IssuesController : BaseController
    {
        public CollectionModel<IssueModel> Get()
        {
            var href = Url.Link("Issues", null);
            return GetCollection(href);
        }

        public IHttpActionResult Get(int id)
        {
            var issue = Context.Issues.FirstOrDefault(x => x.Id == id);

            if (issue == null)
                return NotFound();

            return Result.Object(issue);
        }

        [Route("~/api/boards/{boardId}/issues", Name = "IssuesByBoardId")]
        public CollectionModel<IssueModel> GetByBoardId(int boardId)
        {
            var href = Url.Link("IssuesByBoardId", new { boardId });
            return GetCollection(href, x => x.BoardId == boardId);
        }

        [Route("~/api/boards/{boardId}/issues", Name = "CreateIssueForBoard")]
        public IHttpActionResult Post(int boardId, IssueModel postedModel)
        {
            var issue = new Issue
            {
                Title = postedModel.Title,
                BoardId = boardId,
                StateId = postedModel.StateId
            };

            Context.Issues.Add(issue);
            Context.SaveChanges();

            var issueModel = Result.ToModel(issue);
            ConnectionManager.BroadcastAddIssue(issueModel);

            return Result.Created(issue);
        }

        [Route("~/api/boards/{boardId}/issues/{issueId}/statetransitions", Name = "IssueStateTransitions")]
        public HttpResponseMessage PostStateChange(int boardId, int issueId, IssueModel postedModel)
        {
            var issue = Context.Issues.FirstOrDefault(x => x.Id == issueId && x.BoardId == boardId);

            if (issue == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            issue.StateId = postedModel.StateId;

            Context.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        public IHttpActionResult Put(int id, IssueModel postedModel)
        {
            var issue = Context.Issues.FirstOrDefault(x => x.Id == id);

            if (issue == null)
                return NotFound();

            issue.Title = postedModel.Title;
            issue.Description = postedModel.Description;

            Context.SaveChanges();

            var issueModel = Result.ToModel(issue);
            ConnectionManager.BroadcastUpdateIssue(issueModel);

            return Result.Object(issue);
        }

        public HttpResponseMessage Delete(int id)
        {
            return DeleteById<Issue>(id, x =>
            {
                var issueModel = Result.ToModel(x);
                ConnectionManager.BroadcastDeleteIssue(issueModel);
            });
        }

        private CollectionModel<IssueModel> GetCollection(string href, Expression<Func<Issue, bool>> predicate = null)
        {
            IQueryable<Issue> issues = Context.Issues;

            if (predicate != null)
                issues = issues.Where(predicate);

            return Result.Collection(href, issues);
        }
        private IssueModelResultFactory Result
        {
            get { return new IssueModelResultFactory(Url, this); }
        }
    }
}
