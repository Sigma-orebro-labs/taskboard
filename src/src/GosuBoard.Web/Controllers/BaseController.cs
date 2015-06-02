using GosuBoard.Web.Entities;
using GosuBoard.Web.Infrastructure;
using GosuBoard.Web.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GosuBoard.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult Created(EntityModel model)
        {
            var href = model.GetSelfHref();
            return Created(href, model);
        }

        protected IActionResult DeleteById<T>(int id)
            where T : Entity
        {
            using (var context = new BoardContext())
            {
                var set = context.Set<T>();

                var state = set.FirstOrDefault(x => x.Id == id);

                if (state == null)
                    return new HttpNotFoundResult();

                set.Remove(state);

                context.SaveChanges();
            }

            return new NoContentResult();

        }
    }
}
