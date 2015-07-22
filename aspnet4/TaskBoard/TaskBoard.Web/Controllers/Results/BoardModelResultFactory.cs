using System.Web.Http;
using System.Web.Http.Routing;
using TaskBoard.Domain.Entities;
using TaskBoard.Web.Mapping;
using TaskBoard.Web.Models;

namespace TaskBoard.Web.Controllers.Results
{
    public class BoardModelResultFactory : EntityModelResultFactory<Board, BoardModel>
    {
        private readonly UrlHelper _url;

        public BoardModelResultFactory(UrlHelper url, ApiController controller)
            : base(controller)
        {
            _url = url;
        }

        public override BoardModel ToModel(Board entity)
        {
            var boardModel = entity.ToModel();

            boardModel.AddLink(CreateIssuesLink(entity.Id));
            boardModel.AddLink(CreateStatesLink(entity.Id));
            boardModel.AddLink(CreateSelfLink(entity.Id));

            return boardModel;
        }

        private LinkModel CreateIssuesLink(int id)
        {
            var href = _url.Link("IssuesByBoardId", new { boardId = id });
            return new LinkModel("issues", "Issues", href);
        }

        private LinkModel CreateStatesLink(int id)
        {
            var href = _url.Link("StatesByBoardId", new { boardId = id });
            return new LinkModel("states", "States", href);
        }

        private LinkModel CreateSelfLink(int id)
        {
            var href = _url.Link("DefaultRouting", new { id = id, controller = "boards" });
            return new LinkModel("self", "Self", href);
        }
    }
}
