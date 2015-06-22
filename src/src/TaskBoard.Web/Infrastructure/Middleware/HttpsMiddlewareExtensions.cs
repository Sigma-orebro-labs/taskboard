using Microsoft.AspNet.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskBoard.Web.Infrastructure.Middleware
{
    public static class HttpsRedirectMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpsRedirect(this IApplicationBuilder app)
        {
            return app.Use(next => new HttpsRedirectMiddleware(next).Invoke);
        }
    }
}
