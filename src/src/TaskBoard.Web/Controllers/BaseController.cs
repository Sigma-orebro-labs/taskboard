using TaskBoard.Web.Entities;
using TaskBoard.Web.Infrastructure;
using Microsoft.AspNet.Mvc;
using System;
using System.Linq;

namespace TaskBoard.Web.Controllers
{
    public class BaseController : Controller
    {
        private BoardContext _context;

        protected BaseController(BoardContext boardContext)
        {
            _context = boardContext;
        }

        protected IActionResult DeleteById<T>(int id, Action<T> onDelete = null)
            where T : Entity
        {
            var set = _context.Set<T>();

            var entity = set.FirstOrDefault(x => x.Id == id);

            if (entity == null)
                return new HttpNotFoundResult();

            set.Remove(entity);

            _context.SaveChanges();

            if (onDelete != null)
                onDelete(entity);

            return new NoContentResult();
        }
    }
}
