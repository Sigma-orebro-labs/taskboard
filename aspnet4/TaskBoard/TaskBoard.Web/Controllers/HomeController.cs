using System.Web.Mvc;
using TaskBoard.Web.Controllers.Results;

namespace TaskBoard.Web.Controllers
{
    public class HomeController : Controller
    {
        public TransferResult Index()
        {
            return new TransferResult("~/Content/index.html");
        }
    }
}