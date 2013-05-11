using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Text;

namespace Tank.Request
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class CreateAllXml : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (csFunction.ValidAdminIP(context.Request.UserHostAddress))
            {
                StringBuilder bulid = new StringBuilder();
                //bulid.Append(AchievementList.Bulid(context)); //ok
                bulid.Append(ActiveList.Bulid(context)); //ok               
                bulid.Append(BallList.Bulid(context)); //ok
                bulid.Append(BombConfig.Bulid(context)); //ok
                //bulid.Append(CardBuffList.Bulid(context)); //ok
                //bulid.Append(CardInfoList.Bulid(context)); //ok
                //bulid.Append(CardUpdateCondition.Bulid(context)); //ok
                //bulid.Append(CardUpdateInfo.Bulid(context)); //ok
                //bulid.Append(consortiabuffertemp.Build(context)); //ok
                bulid.Append(ConsortiaLevelList.Bulid(context)); //ok
                bulid.Append(DailyAwardList.Bulid(context)); //ok
                //bulid.Append(dailyleagueaward.Build(context)); //ok
                //bulid.Append(dailyleaguelevel.Build(context)); //ok
                //bulid.Append(DropItemForNewRegister.Build(context)); //ok
                //bulid.Append(exerciseinfolist.Build(context)); //ok
                //bulid.Append(FightLabDropItemList.Build(context)); //ok
                //bulid.Append(ItemStrengthenData.Build(context)); //ok
                bulid.Append(ItemStrengthenList.Bulid(context)); //ok
                //bulid.Append(LevelList.Build(context)); //ok
                //bulid.Append(LoadAllQuestions.Build(context)); //ok
                bulid.Append(LoadBoxTemp.Bulid(context)); //ok
                bulid.Append(LoadItemsCategory.Bulid(context)); //ok
                bulid.Append(LoadMapsItems.Bulid(context)); //ok
                bulid.Append(LoadPVEItems.Build(context)); //ok
                //bulid.Append(LoadUserBox.Build(context)); //ok
                bulid.Append(MapServerList.Bulid(context)); //ok
                bulid.Append(NPCInfoList.Bulid(context)); //ok
                bulid.Append(QuestList.Bulid(context)); //ok
                bulid.Append(ShopGoodsShowList.Bulid(context)); //ok
                bulid.Append(ShopItemList.Bulid(context)); //ok
                bulid.Append(TemplateAllList.Bulid(context)); //ok
                context.Response.ContentType = "text/plain";
                context.Response.Write(bulid.ToString());
              }
            else
            {
                context.Response.Write("IP is not valid!");
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
