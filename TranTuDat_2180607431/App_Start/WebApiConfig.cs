using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http;
using System.Web.Http.Routing;

namespace TranTuDat_2180607431
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Xóa các định tuyến mặc định
            /*
            config.Routes.Clear();

            config.Routes.MapHttpRoute(
                name: "GetAttendance",
                routeTemplate: "api/Attendances",
                defaults: new { controller = "Attendances", action = "Get" }
            );

            config.Routes.MapHttpRoute(
                name: "PostAttendance",
                routeTemplate: "api/Attendances",
                defaults: new { controller = "Attendances", action = "Post" }
            );

            
            config.Routes.MapHttpRoute(
                name: "DeleteAttendance",
                routeTemplate: "api/Attendances/{id}",
                defaults: new { controller = "Attendances", action = "Delete" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) }
            );
            */
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        
    }
}
