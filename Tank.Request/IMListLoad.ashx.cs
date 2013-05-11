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
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class IMListLoad : IHttpHandler
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void ProcessRequest(HttpContext context)
        {
            bool value = false;
            string message = "Fail!";
            XElement result = new XElement("Result");

            try
            {
                string username = context.Request["uname"];
                int id = Convert.ToInt32(context.Request["id"]);
                int selfid = Convert.ToInt32(context.Request["selfid"]);
                string key = context.Request["key"];
                using (PlayerBussiness db = new PlayerBussiness())
                {
                    FriendInfo[] infos = db.GetFriendsAll(selfid);
		//<customList ID="0" Name="Bạn bè" />
  		//<customList ID="1" Name="D.sách đen" />
  		//<customList ID="10" Name="" />
  		//<customList ID="11" Name="" />
  		//<customList ID="12" Name="" />
  		//<customList ID="13" Name="" />
  		//<customList ID="14" Name="" />
  		//<customList ID="15" Name="" />
  		//<customList ID="16" Name="" />
 		//<customList ID="17" Name="" />
  		//<customList ID="18" Name="" />
  		//<customList ID="19" Name="" />
                    XElement node0 = new XElement("customList",
                        new XAttribute("ID", 0),
                        new XAttribute("Name", "Bạn bè"));
                    result.Add(node0);
                    node0 = new XElement("customList",
                        new XAttribute("ID", 1),
                        new XAttribute("Name", "D.sách đen"));
                    result.Add(node0);
                    node0 = new XElement("customList",
                       new XAttribute("ID", 10),
                       new XAttribute("Name", ""));
                    result.Add(node0);
                    node0 = new XElement("customList",
                       new XAttribute("ID", 11),
                       new XAttribute("Name", ""));
                    result.Add(node0);
                    node0 = new XElement("customList",
                       new XAttribute("ID", 12),
                       new XAttribute("Name", ""));
                    result.Add(node0);
                    node0 = new XElement("customList",
                       new XAttribute("ID", 13),
                       new XAttribute("Name", ""));
                    result.Add(node0);
                    node0 = new XElement("customList",
                       new XAttribute("ID", 14),
                       new XAttribute("Name", ""));
                    result.Add(node0);
                    node0 = new XElement("customList",
                       new XAttribute("ID", 15),
                       new XAttribute("Name", ""));
                    result.Add(node0);
                    node0 = new XElement("customList",
                       new XAttribute("ID", 16),
                       new XAttribute("Name", ""));
                    result.Add(node0);
                    node0 = new XElement("customList",
                       new XAttribute("ID", 17),
                       new XAttribute("Name", ""));
                    result.Add(node0);
                    node0 = new XElement("customList",
                       new XAttribute("ID", 18),
                       new XAttribute("Name", ""));
                    result.Add(node0);
                    node0 = new XElement("customList",
                       new XAttribute("ID", 19),
                       new XAttribute("Name", ""));
                    result.Add(node0);
                    foreach (FriendInfo g in infos)
                    {
                        XElement node = new XElement("Item", 
	                    new XAttribute("ID", g.FriendID),
                            new XAttribute("NickName", g.NickName),
                            new XAttribute("LoginName", g.UserName),
                            new XAttribute("Style", g.Style),
                            new XAttribute("Sex", g.Sex == 1 ? true : false),
                            new XAttribute("Colors", g.Colors),
                            new XAttribute("Grade", g.Grade),
                            new XAttribute("Hide", g.Hide),
                            new XAttribute("ConsortiaName", g.ConsortiaName),
                            new XAttribute("TotalCount", g.Total),
                            new XAttribute("EscapeCount", g.Escape),
                            new XAttribute("WinCount", g.Win),
                            new XAttribute("Offer", g.Offer),
                            new XAttribute("Relation", g.Relation),
                            new XAttribute("Repute", g.Repute),
                            new XAttribute("State", g.State == 1 ? 1 : 0),
                            new XAttribute("Nimbus",g.Nimbus),
                            new XAttribute("DutyName", g.DutyName),
		                    new XAttribute("AchievementPoint",0),
		                    new XAttribute("Rank","Chiến sĩ siêu cấp"), 
		                    new XAttribute("FightPower",13528),
		                    new XAttribute("ApprenticeshipState",0),
		                    new XAttribute("BBSFriends",false),
		                    new XAttribute("Birthday",DateTime.Now),
		                    new XAttribute("UserName",g.UserName),
		                    new XAttribute("IsMarried",false));
                        result.Add(node);
                    }
                }

                value = true;
                message = "Success!";
            }
            catch (Exception ex)
            {
                log.Error("IMListLoad", ex);
            }

            result.Add(new XAttribute("value", value));
            result.Add(new XAttribute("message", message));

            context.Response.ContentType = "text/plain";
            context.Response.Write(result.ToString(false));
            //context.Response.BinaryWrite(StaticFunction.Compress(result.ToString(false)));
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
