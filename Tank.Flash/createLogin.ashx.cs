using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Bussiness;
using System.Web.Security;
using System.Web.SessionState;

namespace Tank.Flash
{
    /// <summary>
    /// Summary description for createLogin
    /// </summary>
    public class createLogin : IHttpHandler, IRequiresSessionState
    {

        public string SiteTitle
        {
            get
            {
                return ((ConfigurationManager.AppSettings["SiteTitle"] == null) ? "DanDanTang" : ConfigurationManager.AppSettings["SiteTitle"]);
            }
        }
        public void ProcessRequest(HttpContext context)
        {

            string username = context.Request["username"];
            string password = context.Request["password"];
            using (MemberShipBussiness db = new MemberShipBussiness())
            {
                if (db.CheckUsername(SiteTitle, username, FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5")))
                {
                    context.Session["username"] = username;
                    context.Session["password"] = password;
                    context.Response.Write("ok");
                }
                else
                {
                    context.Response.Write("UserName or Password wrong!");
                }
            }

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