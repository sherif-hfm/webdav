using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace webdavSrv.Controllers
{
    [RoutePrefix("api")]
    public class HomeController : ApiController
    {
        [Route("Test")]
        [HttpGet]
        public IHttpActionResult Test()
        {
            return Content(System.Net.HttpStatusCode.OK, "Web Api Test:" + DateTime.Now.ToString());
        }
    }
}
