using GosuBoard.Web.Entities;
using GosuBoard.Web.Mapping;
using GosuBoard.Web.Models;
using Microsoft.AspNet.Mvc;

namespace GosuBoard.Web.Controllers.Results
{
    public class BoardModelResultFactory : EntityModelResultFactory<Board, BoardModel>
    {
        private readonly IUrlHelper _url;

        public BoardModelResultFactory(IUrlHelper url)
        {
            _url = url;
        }

        public override BoardModel ToModel(Board entity)
        {
            var BoardModel = entity.ToModel();

            BoardModel.AddLink(CreateIssuesLink(entity.Id));
            BoardModel.AddLink(CreateStatesLink(entity.Id));
            BoardModel.AddLink(CreateSelfLink(entity.Id));

            return BoardModel;
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
            var href = _url.Link("BoardById", new { id = id });
            return new LinkModel("self", "Self", href);
        }
    }
}
