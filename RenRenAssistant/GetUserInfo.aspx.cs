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
using Bussiness.Interface;

namespace RenRenAssistant
{
    public partial class GetUserInfo : System.Web.UI.Page
    {
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

        public string UserInfoUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["UserInfoUrl"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string result = "";
            try
            {
                string uid = Request["uid"] == null ? "" : Request["uid"].ToString();

                if (!string.IsNullOrEmpty(uid))
                {
                    if (!string.IsNullOrEmpty(UserInfoUrl))
                    {
                        //key：MD5（code+uid+约定值）
                        string sign = BaseInterface.md5(Code + uid + Key);
                        string param = string.Format("uid={0}&code={1}&key={2}", HttpUtility.UrlEncode(uid), Code, sign);

                        result = BaseInterface.RequestContent(UserInfoUrl + "?" + param);
                        Response.Write(result);
                        return;
                    }                  
                }
                Response.Write("");
            }
            catch
            {
                Response.Write("");
            }
        }
    }
}
