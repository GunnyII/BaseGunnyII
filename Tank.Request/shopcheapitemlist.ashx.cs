using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Specialized;
using log4net;
using System.Reflection;
using Bussiness;

namespace Tank.Request
{
    /// <summary>
    /// Summary description for shopcheapitemlist
    /// </summary>
    public class shopcheapitemlist : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            bool value = true;
            string message = "Success!";           
            XElement result = new XElement("Result");
            result.Add(new XAttribute("value", value));
            result.Add(new XAttribute("message", message));
            context.Response.ContentType = "text/plain";
            context.Response.Write(result.ToString(false));
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