using GosuBoard.Web.Entities;
using GosuBoard.Web.Mapping;
using GosuBoard.Web.Models;
using Microsoft.AspNet.Mvc;

namespace GosuBoard.Web.Controllers.Results
{
    public class IssueStateModelResultFactory : EntityModelResultFactory<IssueState, IssueStateModel>
    {
        private readonly IUrlHelper _url;

        public IssueStateModelResultFactory(IUrlHelper url)
        {
            _url = url;
        }

        public override IssueStateModel ToModel(IssueState entity)
        {
            var IssueStateModel = entity.ToModel();

            IssueStateModel.AddLink(CreateBoardLink(entity.BoardId));
            IssueStateModel.AddLink(CreateSelfLink(entity.Id));

            return IssueStateModel;
        }

        private LinkModel CreateBoardLink(int boardId)
        {
            var href = _url.Link("BoardById", new { id = boardId });
            return new LinkModel("board", "Board", href);
        }

        private LinkModel CreateSelfLink(int id)
        {
            var href = _url.Link("StateById", new { id = id });
            return new LinkModel("self", "Self", href);
        }
    }
}
