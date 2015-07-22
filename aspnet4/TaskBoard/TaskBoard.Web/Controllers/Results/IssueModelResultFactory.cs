using System.Web.Http;
using System.Web.Http.Routing;
using TaskBoard.Domain.Entities;
using TaskBoard.Web.Mapping;
using TaskBoard.Web.Models;

namespace TaskBoard.Web.Controllers.Results
{
    public class IssueModelResultFactory : EntityModelResultFactory<Issue, IssueModel>
    {
        private readonly UrlHelper _url;

        public IssueModelResultFactory(UrlHelper url, ApiController controller)
            : base(controller)
        {
            _url = url;
        }

        public override IssueModel ToModel(Issue entity)
        {
            var issueModel = entity.ToModel();

            issueModel.AddLink(CreateBoardLink(entity.BoardId));
            issueModel.AddLink(CreateIssueStateTransitionsLink(entity.BoardId, entity.Id));
            issueModel.AddLink(CreateSelfLink(entity.Id));

            return issueModel;
        }

        private LinkModel CreateBoardLink(int boardId)
        {
            var href = _url.Link("DefaultRouting", new { id = boardId, controller = "boards" });
            return new LinkModel("board", "Board", href);
        }

        private LinkModel CreateSelfLink(int id)
        {
            var href = _url.Link("DefaultRouting", new { id = id, controller = "issues" });
            return new LinkModel("self", "Self", href);
        }

        private LinkModel CreateIssueStateTransitionsLink(int boardId, int issueId)
        {
            var href = _url.Link("IssueStateTransitions", new { boardId = boardId, issueId = issueId });
            return new LinkModel("statetransitions", "State transitions", href);
        }
    }
}
