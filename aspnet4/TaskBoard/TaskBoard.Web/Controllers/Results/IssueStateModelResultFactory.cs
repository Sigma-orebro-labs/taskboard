using System.Web.Http;
using System.Web.Http.Routing;
using TaskBoard.Domain.Entities;
using TaskBoard.Web.Mapping;
using TaskBoard.Web.Models;

namespace TaskBoard.Web.Controllers.Results
{
    public class IssueStateModelResultFactory : EntityModelResultFactory<IssueState, IssueStateModel>
    {
        private readonly UrlHelper _url;

        public IssueStateModelResultFactory(UrlHelper url, ApiController controller)
            : base(controller)
        {
            _url = url;
        }

        public override IssueStateModel ToModel(IssueState entity)
        {
            var issueStateModel = entity.ToModel();

            issueStateModel.AddLink(CreateBoardLink(entity.BoardId));
            issueStateModel.AddLink(CreateSelfLink(entity.Id));

            return issueStateModel;
        }

        private LinkModel CreateBoardLink(int boardId)
        {
            var href = _url.Link("DefaultRouting", new { id = boardId, controller = "boards" });
            return new LinkModel("board", "Board", href);
        }

        private LinkModel CreateSelfLink(int id)
        {
            var href = _url.Link("DefaultRouting", new { id = id, controller = "issuestates" });
            return new LinkModel("self", "Self", href);
        }
    }
}
