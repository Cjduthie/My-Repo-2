using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Contoso_Uni
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API route & to enable "Cors" support. //
            // Cors is used to send a request from a client to a web service.//
            //IT is also used to allow the APIS (Application Programing Interfaces: Enrollment, Student, Course. //
            var cors =new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web Api Routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
