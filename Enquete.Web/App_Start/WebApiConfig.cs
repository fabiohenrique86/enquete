using System.Web.Http;

namespace Enquete.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services            
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi1",
                routeTemplate: "{controller}/{id}",
                defaults: new { action = "Get" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: "{controller}/{id}/{action}"
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi3",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { action = RouteParameter.Optional, id = RouteParameter.Optional }
            );
        }
    }
}
