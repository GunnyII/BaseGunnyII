using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tank.Request
{
    /// <summary>
    /// Summary description for UpdateVersion
    /// </summary>
    public class UpdateVersion : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("1");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}