using System;
using System.Data;
using System.Configuration;
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
    public enum eChargeError
    {
        /// <summary>
        /// 未知异常
        /// </summary>
        DefaultError = 0x00,

        /// <summary>
        /// 人人网调用失败
        /// </summary>
        RenRenError = 0x01,

        /// <summary>
        /// 内部调用失败
        /// </summary>
        RequestError = 0x02,


    }
}
