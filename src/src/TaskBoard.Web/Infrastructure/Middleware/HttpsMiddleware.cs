using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskBoard.Web.Infrastructure.Middleware
{
    public class HttpsRedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpsRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.IsHttps)
            {
                var httpsUrl = string.Format("https://{0}{1}{2}", context.Request.Host, context.Request.Path, context.Request.QueryString);

                context.Response.Redirect(httpsUrl);
            }
            else
            {
                await _next(context);
            }
        }
    }
}
