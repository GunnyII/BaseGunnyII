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

namespace RenRenAssistant
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //StatMgr.SaveError("billId", "uid", 100, eChargeError.RenRenError.ToString());
            //this.Response.Write("<script language='javascript'>alert('充值失败!');window.close();</script>");
        }
    }
}
