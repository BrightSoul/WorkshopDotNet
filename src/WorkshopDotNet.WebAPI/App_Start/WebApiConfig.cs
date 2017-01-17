using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using WorkshopDotNet.Modello.Entita;

namespace WorkshopDotNet.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Servizi e configurazione dell'API Web

            // Route dell'API Web
            config.MapHttpAttributeRoutes();
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.;
            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel());

           /* config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );*/
        }
    }
}
