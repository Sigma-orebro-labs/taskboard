using TaskBoard.Web.Entities;
using TaskBoard.Web.Infrastructure;
using TaskBoard.Web.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskBoard.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult DeleteById<T>(int id, Action<T> onDelete = null)
            where T : Entity
        {
            using (var context = new BoardContext())
            {
                var set = context.Set<T>();

                var entity = set.FirstOrDefault(x => x.Id == id);

                if (entity == null)
                    return new HttpNotFoundResult();

                set.Remove(entity);

                context.SaveChanges();

                if (onDelete != null)
                    onDelete(entity);
            }

            return new NoContentResult();
        }
    }
}
