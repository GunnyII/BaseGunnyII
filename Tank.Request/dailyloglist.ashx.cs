using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using log4net;
using System.Reflection;
using Bussiness;
using SqlDataProvider.Data;

namespace Tank.Request
{
    /// <summary>
    /// Summary description for dailyloglist
    /// </summary>
    public class dailyloglist : IHttpHandler
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void ProcessRequest(HttpContext context)
        {
            bool value = false;
            string message = "Fail!";
            XElement result = new XElement("Result");

            try
            {
                string key = context.Request["key"];
                int id = int.Parse(context.Request["selfid"]);
                using (PlayerBussiness db = new PlayerBussiness())
                {
                    //DailyLogListInfo[] infos = db.GetDailyLogListById(id);

                    XElement node = new XElement("DailyLogList",
                        new XAttribute("UserAwardLog", "2"),
                        new XAttribute("DayLog", "True,True,False,False,False,False,False,False,False,False,False,False,False,False"));
                    result.Add(node);

                }

                value = true;
                message = "Success!";
            }
            catch (Exception ex)
            {
                log.Error("dailyloglist", ex);
            }

            result.Add(new XAttribute("value", value));
            result.Add(new XAttribute("message", message));
            result.Add(new XAttribute("nowDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));//nowDate="2012-08-16 16:24:17"
            context.Response.ContentType = "text/plain";
            //context.Response.Write(result.ToString(false));
            context.Response.BinaryWrite(StaticFunction.Compress(result.ToString(false)));
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