using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace RecpMgmtWebApi
{
	//public class CustomJsonFormatter : JsonMediaTypeFormatter
	//{
	//	public CustomJsonFormatter()
	//	{
	//		this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
	//	}
	//	public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
	//	{
	//		base.SetDefaultContentHeaders(type, headers, mediaType);
	//		headers.ContentType = new MediaTypeHeaderValue("application/json");
	//	}
	//}
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			// Enabl cors
			var corsAttr = new EnableCorsAttribute("*", "*", "*");
			config.EnableCors(corsAttr);

			// Web API configuration and services
			// Configure Web API to use only bearer token authentication.
			config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes(); config.Routes.MapHttpRoute(
				 name: "AlternateApi",
				 routeTemplate: "api/{controller}/{action}/{id}",
				 defaults: new { id = RouteParameter.Optional }
			 );

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

		//	System.Net.Http.Headers.MediaTypeHeaderValue appXmlType = null;
			//config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
			//	config.Formatters.Remove(config.Formatters.XmlFormatter);
			//	HttpConfiguration config = new HttpConfiguration();
		//	config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
			//config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
		//var json = config.Formatters.JsonFormatter;
		//json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
			config.Formatters.Remove(config.Formatters.XmlFormatter);
		//	config.Formatters.Add(new CustomJsonFormatter());
		//	config.Formatters.Remove(config.Formatters.XmlFormatter);
		//	System.Net.Http.Headers.MediaTypeHeaderValue appXmlType = null;
		//	config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
		//	config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
	}
    }
}
