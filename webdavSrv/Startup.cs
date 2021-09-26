using log4net;
using Newtonsoft.Json;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace webdavSrv
{
    public class Startup
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void Configuration(IAppBuilder app)
        {
            log.Info("Web Start");
            // add web api
            var config = new HttpConfiguration();
            //config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
            name: "webdavApi",
            routeTemplate: "webdav/{*resName}",
            defaults: new { controller = "webdav" }
        );
            config.Routes.MapHttpRoute(
          name: "Api",
          routeTemplate: "api/{*resName}",
          defaults: new { controller = "Home" }
          );
            //config.MessageHandlers.Add(new AutoLogDelegateHandler());
            // allow windows auth in self hosting
            //HttpListener listener = (HttpListener)app.Properties["System.Net.HttpListener"];
            //listener.AuthenticationSchemes = AuthenticationSchemes.IntegratedWindowsAuthentication;
            app.Use(async (context, next) =>
            {
                var req = context.Request.Method;
                log.Info(context.Request.Method);
                if (context.Request.Method == "OPTIONS")
                {
                    // sometimes OPTIONS called without webdav path only "/"
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.Headers.Add("Allow", new string[] { "OPTIONS, TRACE, GET, HEAD, POST, COPY, PROPFIND, DELETE, MOVE, PROPPATCH, MKCOL, LOCK, UNLOCK" });
                    context.Response.Headers.Add("Public", new string[] { "OPTIONS, TRACE, GET, HEAD, POST, PROPFIND, PROPPATCH, MKCOL, PUT, DELETE, COPY, MOVE, LOCK, UNLOCK" });
                    context.Response.Headers.Add("DAV", new string[] { "1,2,3" });
                    context.Response.Headers.Add("MS-Author-Via", new string[] { "DAV" });
                    context.Response.ContentLength = 0;
                    return;
                }
                //if (context.Request.Method == "HEAD")
                //{
                //    context.Response.StatusCode = (int)HttpStatusCode.OK;
                //    context.Response.Headers.Add("ETag", new string[] { "W/\"d35b3112b4add71:0\"" });
                //    context.Response.Headers.Add("Content-Type", new string[] { "application/vnd.openxmlformats-officedocument.wordprocessingml.document" });
                //    context.Response.Headers.Add("Last-Modified", new string[] { "Sun, 19 Sep 2021 19:35:12 GMT" });
                //    context.Response.ContentLength = new System.IO.FileInfo(@"C:\temp\doc1.docx").Length;
                //    return;
                //}
                //if (context.Request.Method == "LOCK")
                //{
                //    context.Response.StatusCode = (int)HttpStatusCode.OK;
                //    context.Response.Headers.Add("Content-Type", new string[] { "text/xml" });
                //    context.Response.Body = new MemoryStream(Encoding.Default.GetBytes(Res1.Lock1));
                //    return;
                //}

                //log.Info(JsonConvert.SerializeObject(context.Request.Headers));
                await next.Invoke();
            });
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}