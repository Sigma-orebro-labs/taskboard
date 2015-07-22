using System;
using System.Web;
using System.Web.Mvc;

namespace TaskBoard.Web.Controllers.Results
{
    public class TransferResult : ActionResult
    {
        public readonly string Url;

        public TransferResult(string url)
        {
            Url = url;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            HttpContext.Current.Server.TransferRequest(this.Url, true);
        }
    }
}