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
using Microsoft.AspNet.Hosting;

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

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment environment)
        {
            loggerFactory.AddConsole();

            if (environment.EnvironmentName.ToLower() == "development")
            {
                app.UseErrorPage(ErrorPageOptions.ShowAll);
            }
            else
            {
                // Force all requests to go through HTTPS in all environments except for development
                app.UseHttpsRedirect();
            }

            app.UseMvc()
               .UseStaticFiles()
               .UseWebSockets()
               .UseSignalR();
        }
    }
}
