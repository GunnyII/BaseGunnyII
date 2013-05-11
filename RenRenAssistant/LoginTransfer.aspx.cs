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
using System.Text;
using Bussiness.Interface;

namespace RenRenAssistant
{
    public partial class LoginTransfer : System.Web.UI.Page
    {
        public static string FlashUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["FlashUrl"];
            }
        }

        public string ValidateUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ValidateUrl"];
            }
        }

        public string LoginOnUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["LoginOnUrl"];
            }
        }

        public string TraceStr
        {
            get
            {
                return ConfigurationManager.AppSettings["TraceStr"];
            }
        }

        public string Code
        {
            get
            {
                return ConfigurationManager.AppSettings["Code"];
            }
        }

        public string Key
        {
            get
            {
                return ConfigurationManager.AppSettings["Key"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string result = "";
            try
            {
                //HttpCookie aCookie = Request.Cookies["societyguester"];
                HttpCookie aCookie = Request.Cookies["t"];
                if (aCookie != null)
                {
                    string value = aCookie.Value;
                    string uid = string.Empty;
                    if (!string.IsNullOrEmpty(ValidateUrl))
                    {
                        //key:  MD5（code + trace + s + 约定值）
                        string sign = BaseInterface.md5(Code + TraceStr + value + Key);
                        string param = string.Format("s={0}&trace={1}&code={2}&key={3}", HttpUtility.UrlEncode(value), TraceStr, Code, sign);

                        result = BaseInterface.RequestContent(ValidateUrl + "?" + param);

                        if (!string.IsNullOrEmpty(result) && result.IndexOf("ok") != -1)
                        {
                            string[] para = result.Split(':');
                            uid = para[1];
                            result = "0";
                        }
                    }
                    else
                    {
                        result = "0";
                    }

                    if (result == "0")
                    {
                        string password = Guid.NewGuid().ToString();
                        int time = BaseInterface.ConvertDateTimeInt(DateTime.Now);
                        string v = BaseInterface.md5(uid + password + time.ToString() + BaseInterface.GetLoginKey);
                        string Url = BaseInterface.LoginUrl + "?content=" + HttpUtility.UrlEncode(uid + "|" + password + "|" + time.ToString() + "|" + v);
                        result = BaseInterface.RequestContent(Url);
                        if (result == "0")
                        {
                            string flashUrl = FlashUrl + "?user=" + HttpUtility.UrlEncode(uid) + "&key=" + HttpUtility.UrlEncode(password);
                            Response.Redirect(flashUrl, false);
                            return;
                        }
                        result = "";
                    }

                }

                Response.Redirect(LoginOnUrl, false);
            }
            catch
            {
                Response.Redirect(LoginOnUrl, false);
            }
        }
    }
}
