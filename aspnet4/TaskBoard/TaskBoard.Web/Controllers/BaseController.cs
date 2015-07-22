using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using TaskBoard.Domain.Entities;
using TaskBoard.Persistence;
using System;
using System.Linq;

namespace TaskBoard.Web.Controllers
{
    public class BaseController : ApiController
    {
        protected readonly BoardContext Context;
        protected readonly IConnectionManager ConnectionManager;

        protected BaseController()
        {
            Context = new BoardContext();
            ConnectionManager = GlobalHost.ConnectionManager;
        }

        protected HttpResponseMessage DeleteById<T>(int id, Action<T> onDelete = null)
            where T : Entity
        {
            var set = Context.Set<T>();

            var entity = set.FirstOrDefault(x => x.Id == id);

            if (entity == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            set.Remove(entity);
            
            Context.SaveChanges();

            if (onDelete != null)
                onDelete(entity);

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
