using System.Linq;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using TaskBoard.Web.Infrastructure;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json.Serialization;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.AspNet.Diagnostics;
using Microsoft.Framework.Logging;
using TaskBoard.Web.Infrastructure.Middleware;

namespace TaskBoard.Web
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .Configure<MvcOptions>(options =>
                {
                    options.OutputFormatters
                        .Select(x => x.Instance)
                        .OfType<JsonOutputFormatter>()
                        .First()
                        .SerializerSettings
                        .ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<BoardContext>();

            services.AddSignalR(options =>
            {
                options.Hubs.EnableDetailedErrors = true;
            });
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

#if !DEBUG
            app.UseHttpsRedirect()
               .UseErrorPage(ErrorPageOptions.ShowAll);
#endif

            app.UseMvc()
               .UseStaticFiles()
               .UseWebSockets()
               .UseSignalR();
        }
    }
}
