using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WorkshopDotNet.Web.MessageHandlers;

namespace WorkshopDotNet.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Servizi e configurazione dell'API Web

            // Route dell'API Web
            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Add(new SuppressRedirectHandler());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
