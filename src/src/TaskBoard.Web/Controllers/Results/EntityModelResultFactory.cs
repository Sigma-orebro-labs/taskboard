using TaskBoard.Web.Models;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace TaskBoard.Web.Controllers.Results
{
    public abstract class EntityModelResultFactory<TEntity, TModel>
        where TModel : EntityModel
    {
        public abstract TModel ToModel(TEntity entity);

        public IActionResult Object(TEntity issue)
        {
            return new ObjectResult(ToModel(issue));
        }

        public CollectionModel<TModel> Collection(string href, IEnumerable<TEntity> entities)
        {
            var models = entities.Select(ToModel);

            return new CollectionModel<TModel>(models, href);
        }

        public IActionResult Created(TEntity entity)
        {
            var model = ToModel(entity);
            var href = model.GetSelfHref();

            return new CreatedResult(href, model);
        }
    }
}
