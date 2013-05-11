using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Tank.Flash
{
    public partial class _Default : System.Web.UI.Page
    {
        private string _content = "";

        public string Content
        {

            get
            {
                return _content;
            }
        }
       
        public string LoginOnUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["LoginOnUrl"];
            }
        }

        public string SiteTitle
        {
            get
            {
                return ConfigurationManager.AppSettings["SiteTitle"] == null ? "DDTank" : ConfigurationManager.AppSettings["SiteTitle"];
            }
        }
        public string Config
        {
            get
            {
                return ConfigurationManager.AppSettings["FlashConfig"];
            }
        }
        public string Flash
        {
            get
            {
                return ConfigurationManager.AppSettings["FlashSite"];
            }
        }
       private string autoParam = "";
        public string AutoParam
        {
            get
            {
                return autoParam;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ("1" == ConfigurationManager.AppSettings["content2"])
                {
                    string contents = Request["content2"];
                    if (!string.IsNullOrEmpty(contents))
                    {
                        _content = contents;
                    }
                    else
                    {
                        Response.Redirect(LoginOnUrl, false);
                    }
                }
                else
                {
                    string user = HttpUtility.UrlDecode(Request["user"]);
                    string key = HttpUtility.UrlDecode(Request["key"]);
                    string config = HttpUtility.UrlDecode(Request["config"]);
                    string editby = (Request["editby"] == null) ? "" : HttpUtility.UrlDecode(Request["editby"]);
                    if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(key))
                    {
                       
                        _content = "user=" + HttpUtility.UrlEncode(user) + "&key=" + HttpUtility.UrlEncode(key);
                        autoParam = "editby=" + HttpUtility.UrlEncode(editby);
                    }
                    else
                    {
                        Response.Redirect(LoginOnUrl, false);
                    }
                }
            }
            catch
            {
                Response.Redirect(LoginOnUrl, false);
            }

        }
    }
}
