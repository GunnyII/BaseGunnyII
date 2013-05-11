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
    /// Summary description for FarmGetUserFieldInfos
    /// </summary>
    public class FarmGetUserFieldInfos : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //<Result value="true" message="Success!">
            //<Item UserID="740619823">
            //<Item SeedID="332111" AcclerateDate="0" GrowTime="2012-08-21T12:07:48" />
            //<Item SeedID="332111" AcclerateDate="0" GrowTime="2012-08-21T12:07:44" />
            //<Item SeedID="332111" AcclerateDate="0" GrowTime="2012-08-21T12:07:49" />
            //<Item SeedID="332112" AcclerateDate="0" GrowTime="2012-08-21T12:07:56" />
            //<Item SeedID="332112" AcclerateDate="0" GrowTime="2012-08-21T12:07:53" />
            //<Item SeedID="332112" AcclerateDate="0" GrowTime="2012-08-21T12:07:54" />
            //<Item SeedID="332112" AcclerateDate="0" GrowTime="2012-08-21T12:07:56" />
            //</Item>

            bool value = true;
            string message = "Success!";            
            XElement result = new XElement("Result");
            XElement node = new XElement("Item");            
            for (int i = 0; i < 16; i++ )
            {
                XElement Item = new XElement("Item",
                    new XAttribute("SeedID", 332111),
                    new XAttribute("AcclerateDate", 0),
                    new XAttribute("GrowTime", "2012-08-21T12:07:48"));

                node.Add(Item);
            }

            node.Add(new XAttribute("UserID", 3));            
            result.Add(node);

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