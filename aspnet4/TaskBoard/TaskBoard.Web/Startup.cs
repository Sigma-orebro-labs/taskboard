using System.Web.Http;
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;
using TaskBoard.Web;

[assembly: OwinStartup(typeof(Startup))]

namespace TaskBoard.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(ConfigureWebApi());

            app.MapSignalR();
        }

        private HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultRouting",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return config;
        }
    }
}