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
    public class IMRecentContactsList : IHttpHandler
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void ProcessRequest(HttpContext context)
        {
            bool value = false;
            string message = "Fail!";
            XElement result = new XElement("Result");
            //id=4&key=8b4b74954e42c4c350764b0f8f513899&recentContacts=0&rnd=0%2E9269368425011635&selfid=4

            try
            {
                int recentContacts = Convert.ToInt32(context.Request["recentContacts"]);
                int id = Convert.ToInt32(context.Request["id"]);
                int selfid = Convert.ToInt32(context.Request["selfid"]);
                string key = context.Request["key"];
                using (PlayerBussiness db = new PlayerBussiness())
                {
                    FriendInfo[] infos = db.GetFriendsAll(selfid);                   
                    
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
                            new XAttribute("Nimbus", g.Nimbus),
                            new XAttribute("DutyName", g.DutyName),
                            new XAttribute("AchievementPoint", 0),
                            new XAttribute("Rank", "Chiến sĩ siêu cấp"),
                            new XAttribute("FightPower", 13528),
                            new XAttribute("ApprenticeshipState", 0),
                            new XAttribute("BBSFriends", false),
                            new XAttribute("Birthday", DateTime.Now),
                            new XAttribute("UserName", g.UserName),
                            new XAttribute("IsMarried", false));
                        result.Add(node);
                    }
                }

                value = true;
                message = "Success!";
            }
            catch (Exception ex)
            {
                log.Error("IMRecentContactsList", ex);
            }
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
