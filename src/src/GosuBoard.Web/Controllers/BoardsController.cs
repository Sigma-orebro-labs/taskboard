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
    [Route("api/boards", Name = "BoardsController")]
    public class BoardsController : Controller
    {
        [HttpGet]
        public CollectionModel<BoardModel> Get()
        {
            using (var context = new BoardContext())
            {
                var boards = context.Boards.ToList();
                var models = boards.ToModels();
                var href = CreateDefaultLink<BoardsController>();

                foreach (var model in models)
                {
                    model.Links.Add(CreateIssuesLink(model.Id));
                    model.Links.Add(CreateSelfLink(model.Id));
                }

                return Collection(models);
            }
        }

        [HttpGet("{id}", Name = "GetBoardById")]
        public IActionResult Get(int id)
        {
            using (var context = new BoardContext())
            {
                var board = context.Boards.FirstOrDefault(x => x.Id == id);

                if (board == null)
                    return HttpNotFound();

                var model = board.ToModel();

                model.Links.Add(CreateIssuesLink(model.Id));
                model.Links.Add(CreateSelfLink(model.Id));

                return new ObjectResult(model);
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

            var newResourceHref = Url.Link("GetBoardById", new { id = board.Id });

            return Created(newResourceHref, boardModel);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string name)
        {
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var context = new BoardContext())
            {
                var board = context.Boards.FirstOrDefault(x => x.Id == id);

                if (board == null)
                    return new HttpNotFoundResult();

                context.Boards.Remove(board);

                context.SaveChanges();
            }

            return new NoContentResult();
        }
        
        private string CreateDefaultLink<T>()
        {
            return Url.Link("BoardsController", null);
        }

        private LinkModel CreateIssuesLink(int id)
        {
            var href = Url.Link("BoardIssues", new { boardId = id });
            return new LinkModel("issues", "Issues", href);
        }

        private LinkModel CreateSelfLink(int id)
        {
            var href = Url.Link("GetBoardById", new { id = id });
            return new LinkModel("self", "Self", href);
        }

        private CollectionModel<T> Collection<T>(IEnumerable<T> items)
        {
            return new CollectionModel<T>(items, CreateDefaultLink<BoardsController>());
        }
    }
}
