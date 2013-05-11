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
    public partial class Pay : System.Web.UI.Page
    {
        public static string FlashUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["FlashUrl"];
            }
        }

        public string PayUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["PayUrl"];
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

        public string PayKey
        {
            get
            {
                return ConfigurationManager.AppSettings["PayKey"];
            }
        }

        public string GameName
        {
            get
            {
                return ConfigurationManager.AppSettings["GameName"];
            }
        }

        public string ChargeUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ChargeUrl"];
            }
        }

        public string UserAccountUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["UserAccountUrl"];
            }
        }

        public string Key
        {
            get
            {
                return ConfigurationManager.AppSettings["Key"];
            }
        }

        //http://pay.renren.com/service/expend.do?uid=XXX&code=XXX&name=XXX&trace=XXX&money=XXX&billId=XXX&key=XXX
        //key:md5(code + uid + trace + money + billId + 约定值)
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refresh_Account();
            }
        }

        private void Refresh_Account()
        {
            string result = "";
            try
            {
                string uid = Request["uid"] == null ? "" : Request["uid"].ToString();
                txtChannel.Text = Request["serverid"] == null ? "" : Request["serverid"].ToString();

                if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(UserAccountUrl))
                {
                    //key: md5(code + uid +约定值)
                    string sign = BaseInterface.md5(Code + uid + Key);
                    string param = string.Format("uid={0}&code={1}&key={2}", HttpUtility.UrlEncode(uid), Code, sign);

                    result = BaseInterface.RequestContent(UserAccountUrl + "?" + param);
                    tdAccount.InnerText = result;
                }
                else
                {
                    tdAccount.InnerText = "0";
                }
            }
            catch
            {
                tdAccount.InnerText = "0";
            }
        }

        private void Charge_Submit()
        {
            string result = "";
            try
            {
                string uid = Request["uid"] == null ? "" : Request["uid"].ToString();

                if (!string.IsNullOrEmpty(uid))
                {
                    int money = int.Parse(select.Value);
                    int needMoney = money / 100;
                    if (needMoney <= 0)
                        return;

                    string billId = Guid.NewGuid().ToString();
                    string payWay = "default";

                    if (!string.IsNullOrEmpty(PayUrl))
                    {
                        string sign = BaseInterface.md5(Code + uid + TraceStr + needMoney + billId + PayKey);
                        string param = string.Format("uid={0}&code={1}&name={2}&trace={3}&money={4}&billId={5}&key={6}", HttpUtility.UrlEncode(uid), Code, HttpUtility.UrlEncode(GameName), TraceStr, needMoney, billId, sign);

                        result = BaseInterface.RequestContent(PayUrl + "?" + param);
                    }
                    else
                    {
                        result = "0";
                    }

                    if (result == "0")
                    {
                        string v = BaseInterface.md5(billId + uid + money + payWay + needMoney + BaseInterface.GetChargeKey);
                        string Url = ChargeUrl + "?content=" + billId + "|" + uid + "|" + money + "|" + payWay + "|" + needMoney + "|" + v;
                        result = BaseInterface.RequestContent(Url);
                        if (result == "0")
                        {
                            this.Response.Write("<script language='javascript'>alert('充值成功!');window.close();</script>");
                        }
                        else
                        {
                            StatMgr.SaveError(billId, uid, needMoney, eChargeError.RequestError.ToString());
                            this.Response.Write("<script language='javascript'>alert('充值异常,请联系GM!');</script>");
                        }
                        return;
                    }
                    else
                    {
                        StatMgr.SaveError(billId, uid, needMoney, eChargeError.RenRenError.ToString());
                    }
                }
            }
            catch
            {
                StatMgr.SaveError("billId", "uid", 0, eChargeError.DefaultError.ToString());
            }
            this.Response.Write("<script language='javascript'>alert('充值失败!');</script>");
        }

        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
            Charge_Submit();
            Refresh_Account();
        }

    }
}
