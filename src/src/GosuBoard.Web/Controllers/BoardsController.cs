using GosuBoard.Web.Entities;
using GosuBoard.Web.Extensions;
using GosuBoard.Web.Infrastructure;
using GosuBoard.Web.Mapping;
using GosuBoard.Web.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GosuBoard.Web.Controllers
{
    [Route("api/[controller]")]
    public class BoardsController : BaseController
    {
        [HttpGet(Name = "Boards")]
        public CollectionModel<BoardModel> Get()
        {
            using (var context = new BoardContext())
            {
                var models = context.Boards.Select(ToModel);
                
                return new CollectionModel<BoardModel>(models, Url.Link("Boards", null));
            }
        }

        [HttpGet("{id}", Name = "BoardById")]
        public IActionResult Get(int id)
        {
            using (var context = new BoardContext())
            {
                var board = context.Boards.FirstOrDefault(x => x.Id == id);

                if (board == null)
                    return HttpNotFound();

                return new ObjectResult(ToModel(board));
            }
        }

        [HttpPost]
        public IActionResult Post(string name)
        {
            var board = new Board
            {
                Name = name
            };

            using (var context = new BoardContext())
            {
                context.Boards.Add(board);

                context.SaveChanges();
            }

            var boardModel = board.ToModel();

            boardModel.Links.Add(CreateIssuesLink(board.Id));
            boardModel.Links.Add(CreateSelfLink(board.Id));

            var newResourceHref = Url.Link("BoardById", new { id = board.Id });

            return Created(newResourceHref, boardModel);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string name)
        {
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return DeleteById<Board>(id);
        }

        private LinkModel CreateIssuesLink(int id)
        {
            var href = Url.Link("IssuesByBoardId", new { boardId = id });
            return new LinkModel("issues", "Issues", href);
        }

        private LinkModel CreateSelfLink(int id)
        {
            var href = Url.Link("BoardById", new { id = id });
            return new LinkModel("self", "Self", href);
        }

        private BoardModel ToModel(Board board)
        {
            var model = board.ToModel();

            model.Links.Add(CreateIssuesLink(model.Id));
            model.Links.Add(CreateSelfLink(model.Id));

            return model;
        }
    }
}
