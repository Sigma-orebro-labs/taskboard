using GosuBoard.Web.Entities;
using GosuBoard.Web.Mapping;
using GosuBoard.Web.Models;
using Microsoft.AspNet.Mvc;

namespace GosuBoard.Web.Controllers.Results
{
    public class IssueModelResultFactory : EntityModelResultFactory<Issue, IssueModel>
    {
        private readonly IUrlHelper _url;

        public IssueModelResultFactory(IUrlHelper url)
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
            var href = _url.Link("BoardById", new { id = boardId });
            return new LinkModel("board", "Board", href);
        }

        private LinkModel CreateSelfLink(int id)
        {
            var href = _url.Link("IssueById", new { id = id });
            return new LinkModel("self", "Self", href);
        }

        private LinkModel CreateIssueStateTransitionsLink(int boardId, int issueId)
        {
            var href = _url.Link("IssueStateTransitions", new { boardId = boardId, issueId = issueId });
            return new LinkModel("statetransitions", "State transitions", href);
        }
    }
}
