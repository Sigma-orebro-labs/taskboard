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
                    model.IssuesLink = CreateIssuesLink(model.Id);
                }

                return Collection(models);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using (var context = new BoardContext())
            {
                var board = context.Boards.FirstOrDefault(x => x.Id == id);

                if (board == null)
                    return HttpNotFound();

                var model = board.ToModel();

                model.IssuesLink = CreateIssuesLink(model.Id);

                return new ObjectResult(model);
            }
        }

        [HttpPost]
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
        
        private string CreateDefaultLink<T>()
        {
            return Url.Link("BoardsController", null);
        }

        private LinkModel CreateIssuesLink(int id)
        {
            var href = Url.Link("BoardIssues", new { boardId = id });
            return new LinkModel("issues", "Issues", href);
        }

        private CollectionModel<T> Collection<T>(IEnumerable<T> items)
        {
            return new CollectionModel<T>(items, CreateDefaultLink<BoardsController>());
        }
    }
}
