using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace webdavSrv
{
    public class AutoLogDelegateHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var requestBody = request.Content.ReadAsStringAsync().Result;

            return await base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    HttpResponseMessage response = task.Result;
                    //if (request.Method == HttpMethod.Options)
                    //{
                    //    response.Content.Headers.Add("Allow", "OPTIONS, TRACE, GET, HEAD, POST, COPY, PROPFIND, DELETE, MOVE, PROPPATCH, MKCOL, LOCK, UNLOCK");
                    //    response.Content.Headers.Add("Public", "OPTIONS, TRACE, GET, HEAD, POST, PROPFIND, PROPPATCH, MKCOL, PUT, DELETE, COPY, MOVE, LOCK, UNLOCK");
                    //    response.Headers.Add("DAV", "1,2,3");
                    //    response.Headers.Add("MS-Author-Via", "DAV");
                    //}
                    //if (request.Method == new HttpMethod("HEAD"))
                    //{
                    //    request.Method = HttpMethod.Get;
                    //    request.Properties.Add("Method", "HEAD");
                    //    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                    //    response.Content.Headers.Add("Last-Modified", "Sun, 19 Sep 2021 19:35:12 GMT");
                    //    response.Headers.Add("ETag", "W/\"14a1ba990add71:0\"");
                    //}
                    //response.StatusCode = System.Net.HttpStatusCode.OK;
                    return response;
                });
        }
    }
}