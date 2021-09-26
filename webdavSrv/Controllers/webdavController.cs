using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Web.Http;

namespace webdavSrv.Controllers
{
    //[RoutePrefix("webdav")]
    public class webdavController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string basePath = @"c:\temp";
        private static string baseUrl = @"http://localhost:5011/webdav";

        [HttpGet]
        [Route("webdav/{resName}")]
        public HttpResponseMessage Get(string resName)
        {
            log.Info("Get");
            string fullName = basePath + "\\" + resName;
            var fileInfo = new System.IO.FileInfo(fullName);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new FileStream(fullName, FileMode.Open, FileAccess.Read));
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = resName;
            response.Content.Headers.ContentLength = fileInfo.Length;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(System.Web.MimeMapping.GetMimeMapping(fullName));
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            return response;
        }

        [HttpOptions]
        [Route("webdav")]
        public HttpResponseMessage Options()
        {
            log.Info("Options");
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new MemoryStream(Encoding.Default.GetBytes("")));
            response.Content.Headers.Add("Allow", new string[] { "OPTIONS, TRACE, GET, HEAD, POST, COPY, PROPFIND, DELETE, MOVE, PROPPATCH, MKCOL, LOCK, UNLOCK" });
            response.Headers.Add("Public", new string[] { "OPTIONS, TRACE, GET, HEAD, POST, PROPFIND, PROPPATCH, MKCOL, PUT, DELETE, COPY, MOVE, LOCK, UNLOCK" });
            response.Headers.Add("DAV", new string[] { "1,2,3" });
            response.Headers.Add("MS-Author-Via", new string[] { "DAV" });
            return response;
        }

        [AcceptVerbs("HEAD")]
        [Route("webdav/{resName}")]
        public HttpResponseMessage Head(string resName)
        {
            log.Info("Head");
            string fullName = basePath + "\\" + resName;
            var fileInfo = new System.IO.FileInfo(fullName);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new MemoryStream(Encoding.Default.GetBytes("")));
            response.Headers.Add("ETag", "W/\"d35b3112b4add71:0\"");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(System.Web.MimeMapping.GetMimeMapping(fullName));
            response.Content.Headers.ContentLength = fileInfo.Length;
            response.Content.Headers.Add("Last-Modified", fileInfo.LastWriteTime.ToString("ddd, dd MMM yyyy HH: mm:ss 'GMT'"));
            return response;
        }

        [AcceptVerbs("LOCK")]
        [Route("webdav/{resName}")]
        public HttpResponseMessage Lock(string resName) 
        {
            log.Info("Lock");
            //WindowsPrincipal winUser = RequestContext.Principal as WindowsPrincipal;
            var lockInfoStr = Encoding.UTF8.GetString(Request.Content.ReadAsByteArrayAsync().Result);
            //var lockReq = Helper.Deserialize<lockinfo>(lockInfoStr);
            string fullName = basePath + "\\" + resName;
            string user = System.IO.File.GetAccessControl(fullName).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK); // to make office file read only make it MethodNotAllowed
            lockResInfo req = new lockResInfo();
            req.lockdiscovery = new lockdiscovery();
            req.lockdiscovery.activelock = new activelock() { depth = "0", timeout = "Second-3600" };
            req.lockdiscovery.activelock.locktype = new locktype() { write = new object() };
            req.lockdiscovery.activelock.lockscope = new lockscope() { exclusive = new object() };
            req.lockdiscovery.activelock.owner = new hrefInfo() { href = user };
            string reqStr = Helper.Serialize<lockResInfo>(req);
            response.Content = new StreamContent(new MemoryStream(Encoding.Default.GetBytes(reqStr)));
            response.Content.Headers.Add("Content-Type", "text/xml");
            response.Content.Headers.ContentLength = Encoding.Default.GetBytes(reqStr).Length;
            return response;
        }


        [AcceptVerbs("PROPFIND")]
        public HttpResponseMessage PROPFIND(string resName)
        {
            string fullName = "";
            log.Info("PropFind");
            if (string.IsNullOrEmpty(resName))
            {
                resName = "/";
                fullName = basePath ;
            }
            else
            {
                fullName = basePath + "\\" + resName;
            }
            

            PropfindRes res = new PropfindRes();
            res.response = new List<response>();
            if (Helper.FileOrDirectoryExists(fullName))
            {
                FileAttributes attr = File.GetAttributes(fullName);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    //directory
                    var dirRes= Helper.GetResponse(resName, basePath, baseUrl);
                    res.response.Add(dirRes);
                  if(this.Request.Headers.Contains("Depth") && this.Request.Headers.GetValues("Depth").First()=="1")
                    {
                        foreach (string file in Directory.GetFileSystemEntries(fullName))
                        {
                            var fileInfo = new System.IO.FileInfo(file);
                            response fileRes = Helper.GetResponse((resName == "/" ? "" : resName + "/") + fileInfo.Name, basePath, baseUrl);
                            res.response.Add(fileRes);
                        } 
                    }
                }
                else
                {
                    //file
                    var fileInfo = new System.IO.FileInfo(fullName);
                    response fileRes = Helper.GetResponse(resName, basePath, baseUrl);
                    res.response.Add(fileRes);
                }
            }
            else
            {
                response fileRes = new response() { propstat = new propstat() { status = "HTTP/1.1 404 Not Found" } };
            }

            HttpResponseMessage response = new HttpResponseMessage((HttpStatusCode)207);
            response.ReasonPhrase = "Multi Status";
            string reqStr = Helper.Serialize<PropfindRes>(res);
            response.Content = new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(reqStr)));
            response.Content.Headers.Add("Content-Type", "text/xml");
            response.Content.Headers.ContentLength = Encoding.UTF8.GetBytes(reqStr).Length;
            return response;
        }

        [AcceptVerbs("UNLOCK")]
        [Route("webdav/{resName}")]
        public HttpResponseMessage Unlock(string resName)
        {
            log.Info("Unlock");
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NoContent);
            var lockToken=this.Request.Headers.GetValues("Lock-Token").First();
            return response;
        }

        [HttpPut]
        [Route("webdav/{resName}")]
        public HttpResponseMessage PUT(string resName)
        {
            log.Info("Put");
            var data = this.Request.Content.ReadAsByteArrayAsync().Result;
            File.WriteAllBytes(@"c:\temp\doc1.docx", data);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NoContent);
            return response;
        }


        [AcceptVerbs("COPY")]        
        [Route("webdav/{resName}")]
        public HttpResponseMessage COPY(string resName)
        {
            return this.Request.CreateResponse(HttpStatusCode.OK, resName);
        }

        [HttpDelete]
        [Route("webdav/{resName}")]
        public HttpResponseMessage Delete(string resName)
        {
            return this.Request.CreateResponse(HttpStatusCode.OK, resName);
        }

        [AcceptVerbs("MKCOL")]
        [Route("webdav/{resName}")]
        public HttpResponseMessage MKCOL(string resName)
        {
            return this.Request.CreateResponse(HttpStatusCode.OK, resName);
        }

        [AcceptVerbs("MOVE")]
        [Route("webdav/{resName}")]
        public HttpResponseMessage MOVE(string resName)
        {
            return this.Request.CreateResponse(HttpStatusCode.OK, resName);
        }
        

        [AcceptVerbs("PROPPATCH")]
        [Route("webdav/{resName}")]
        public HttpResponseMessage PROPPATCH(string resName)
        {
            return this.Request.CreateResponse(HttpStatusCode.OK, resName);
        }

    }
}
