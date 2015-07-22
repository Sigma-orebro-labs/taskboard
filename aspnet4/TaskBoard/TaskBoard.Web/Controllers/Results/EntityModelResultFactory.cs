using System;
using System.Web.Http;
using System.Web.Http.Results;
using TaskBoard.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace TaskBoard.Web.Controllers.Results
{
    public abstract class EntityModelResultFactory<TEntity, TModel>
        where TModel : EntityModel
    {
        private readonly ApiController _controller;

        public abstract TModel ToModel(TEntity entity);

        protected EntityModelResultFactory(ApiController controller)
        {
            _controller = controller;
        }

        public IHttpActionResult Object(TEntity issue)
        {
            return new OkNegotiatedContentResult<TModel>(ToModel(issue), _controller);
        }

        public CollectionModel<TModel> Collection(string href, IEnumerable<TEntity> entities)
        {
            var models = entities.Select(ToModel);

            return new CollectionModel<TModel>(models, href);
        }

        public IHttpActionResult Created(TEntity entity)
        {
            var model = ToModel(entity);
            var href = model.GetSelfHref();

            return new CreatedNegotiatedContentResult<TModel>(new Uri(href, UriKind.RelativeOrAbsolute), model, _controller);
        }
    }
}
