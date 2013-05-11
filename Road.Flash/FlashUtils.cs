using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SqlDataProvider.Data;

namespace Road.Flash
{
    public class FlashUtils
    {
        public static XElement CreateServerInfo(int id, string name, string ip, int port, int state, int mustLevel, int lowestLevel, int online)
        {
            return new XElement("Item", 
                new XAttribute("ID", id),
                new XAttribute("Name", name),
                new XAttribute("IP", ip),
                new XAttribute("Port", port),
                new XAttribute("State", state),
                new XAttribute("MustLevel", mustLevel),
                new XAttribute("LowestLevel", lowestLevel),
                new XAttribute("Online", online),
                new XAttribute("Remark", ""));
        }

        public static XElement CreateMapInfo(MapInfo m)
        {
            return new XElement("Item", new XAttribute("ID", m.ID),
                new XAttribute("Name", m.Name == null ? "" : m.Name),
                new XAttribute("Description", m.Description == null ? "" : m.Description),
                new XAttribute("ForegroundWidth", m.ForegroundWidth),
                new XAttribute("ForegroundHeight", m.ForegroundHeight),
                new XAttribute("BackroundWidht", m.BackroundWidht),
                new XAttribute("BackroundHeight", m.BackroundHeight),
                new XAttribute("DeadWidth", m.DeadWidth),
                new XAttribute("DeadHeight", m.DeadHeight),
                new XAttribute("Weight", m.Weight),
                new XAttribute("DragIndex", m.DragIndex),
                new XAttribute("ForePic", m.ForePic == null ? "" : m.ForePic),
                new XAttribute("BackPic", m.BackPic == null ? "" : m.BackPic),
                new XAttribute("DeadPic", m.DeadPic == null ? "" : m.DeadPic),
                new XAttribute("Pic", m.Pic == null ? "" : m.Pic),
                new XAttribute("BackMusic", m.BackMusic == null ? "" : m.BackMusic),
                new XAttribute("Remark", m.Remark == null ? "" : m.Remark),
                new XAttribute("Type", m.Type));
        }

        public static XElement CreatePveInfo(PveInfo m)
        {
            return new XElement("Item", new XAttribute("ID", m.ID),
                new XAttribute("Name", m.Name == null ? "" : m.Name),
                new XAttribute("Type", m.Type),
                new XAttribute("LevelLimits", m.LevelLimits),
                //LevelLimits="14" 
                new XAttribute("SimpleTemplateIds", m.SimpleTemplateIds == null ? "" : m.SimpleTemplateIds),
                new XAttribute("NormalTemplateIds", m.NormalTemplateIds == null ? "" : m.NormalTemplateIds),
                new XAttribute("HardTemplateIds", m.HardTemplateIds == null ? "" : m.HardTemplateIds),
                new XAttribute("TerrorTemplateIds", m.TerrorTemplateIds == null ? "" : m.TerrorTemplateIds),
                new XAttribute("Pic", m.Pic == null ? "" : m.Pic),
                new XAttribute("Description", m.Description == null ? "" : m.Description),
                new XAttribute("Ordering", m.Ordering),
                new XAttribute("AdviceTips", m.AdviceTips == null ? "" : m.AdviceTips));
                //Ordering="3"
                //AdviceTips="14-16|16-18|18-23|23" />
        }

        public static XElement CreateStrengthenInfo(StrengthenInfo info)
        {
            return new XElement("Item", new XAttribute("StrengthenLevel", info.StrengthenLevel),
                new XAttribute("Rock", info.Rock),
            //Rock1="2" 
            new XAttribute("Rock1", info.Rock1),
            // Rock2="2"
            new XAttribute("Rock2", info.Rock2),
            //Rock3="2" 
            new XAttribute("Rock3", info.Rock3),
            //StoneLevelMin="1"
            new XAttribute("StoneLevelMin", info.StoneLevelMin));
        }
        public static XElement CreateShopGoodsShowListInfo(ShopGoodsShowListInfo info)
        {
            return new XElement("Item", new XAttribute("Type", info.Type),
                   new XAttribute("ShopId", info.ShopId));
        }
        public static XElement CreateItemBoxInfo(ItemBoxInfo info)
        {
            return new XElement("Item", new XAttribute("ID", info.Id),
                new XAttribute("TemplateId", info.TemplateId),
                new XAttribute("StrengthenLevel", info.StrengthenLevel),
                new XAttribute("IsBind", info.IsBind),
                new XAttribute("ItemCount", info.ItemCount),
                new XAttribute("LuckCompose", info.LuckCompose),
                new XAttribute("DefendCompose", info.DefendCompose),
                new XAttribute("AttackCompose", info.AttackCompose),
                new XAttribute("AgilityCompose", info.AgilityCompose),
                new XAttribute("ItemValid", info.ItemValid),
                new XAttribute("IsTips", info.IsTips));
        }
        public static XElement CreateItemInfo(ItemTemplateInfo info)
        {
            return new XElement("Item", new XAttribute("AddTime", info.AddTime),
                new XAttribute("Agility", info.Agility),
                new XAttribute("Attack", info.Attack),
                new XAttribute("CanCompose", info.CanCompose),
                new XAttribute("CanDelete", info.CanDelete),
                new XAttribute("CanDrop", info.CanDrop),
                new XAttribute("CanEquip", info.CanEquip),
                new XAttribute("CanStrengthen", info.CanStrengthen),
                new XAttribute("CanUse", info.CanUse),
                new XAttribute("CategoryID", info.CategoryID),
                new XAttribute("Colors", info.Colors),
                new XAttribute("Defence", info.Defence),
                new XAttribute("Description", info.Description == null ? "" : info.Description),
                new XAttribute("Level", info.Level),
                new XAttribute("Luck", info.Luck),
                new XAttribute("MaxCount", info.MaxCount),
                new XAttribute("Name", info.Name == null ? "" : info.Name),
                new XAttribute("NeedLevel", info.NeedLevel),
                new XAttribute("NeedSex", info.NeedSex),
                new XAttribute("Pic", info.Pic == null ? "" : info.Pic),
                new XAttribute("Data", info.Data == null ? "" : info.Data),
                new XAttribute("Property1", info.Property1),
                new XAttribute("Property2", info.Property2),
                new XAttribute("Property3", info.Property3),
                new XAttribute("Property4", info.Property4),
                new XAttribute("Property5", info.Property5),
                new XAttribute("Property6", info.Property6),
                new XAttribute("Property7", info.Property7),
                new XAttribute("Property8", info.Property8),
                new XAttribute("Quality", info.Quality),
                new XAttribute("Script", info.Script == null ? "" : info.Script),
                new XAttribute("BindType", info.BindType),
                new XAttribute("FusionType", info.FusionType),
                new XAttribute("FusionRate", info.FusionRate),
                new XAttribute("FusionNeedRate", info.FusionNeedRate),
                new XAttribute("TemplateID", info.TemplateID),
                new XAttribute("RefineryLevel", info.RefineryLevel),
                new XAttribute("Hole", info.Hole),
                new XAttribute("ReclaimValue", info.ReclaimValue),
                new XAttribute("ReclaimType", info.ReclaimType),
                new XAttribute("CanRecycle", info.CanRecycle));
        }

        public static XElement CreateGoodsInfo(ItemInfo info)
        {
            return new XElement("Item", new XAttribute("AgilityCompose", info.AgilityCompose),
                new XAttribute("AttackCompose", info.AttackCompose),
                new XAttribute("BeginDate", info.BeginDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new XAttribute("Color", info.Color == null ? "" : info.Color),
                new XAttribute("Skin", info.Skin == null ? "" : info.Skin),
                new XAttribute("Count", info.Count),
                new XAttribute("DefendCompose", info.DefendCompose),
                new XAttribute("IsBinds", info.IsBinds),
                new XAttribute("IsUsed", info.IsUsed),
                new XAttribute("IsJudge", info.IsJudge),
                new XAttribute("ItemID", info.ItemID),
                new XAttribute("LuckCompose", info.LuckCompose),
                new XAttribute("Place", info.Place),
                new XAttribute("StrengthenLevel", info.StrengthenLevel),
                new XAttribute("TemplateID", info.TemplateID),
                new XAttribute("UserID", info.UserID),
                new XAttribute("BagType", info.BagType),
                new XAttribute("ValidDate", info.ValidDate),
                new XAttribute("Hole1", info.Hole1),
                new XAttribute("Hole2", info.Hole2),
                new XAttribute("Hole3", info.Hole3),
                new XAttribute("Hole4", info.Hole4),
                new XAttribute("Hole5", info.Hole5),
                new XAttribute("Hole6", info.Hole6));
        }

        public static XElement CreateShopInfo(ShopItemInfo shop)
        {
            return new XElement("Item", new XAttribute("ID", shop.ID),
                new XAttribute("ShopID", shop.ShopID),
                // GroupID="0" 
                new XAttribute("GroupID", shop.GroupID),
                new XAttribute("TemplateID", shop.TemplateID),
                new XAttribute("BuyType", shop.BuyType),
                new XAttribute("IsContinue", shop.IsContinue),
                new XAttribute("IsBind", shop.IsBind),
                new XAttribute("IsVouch", shop.IsVouch),
                new XAttribute("Label", shop.Label),
                new XAttribute("Beat", shop.Beat),
                new XAttribute("AUnit", shop.AUnit),
                new XAttribute("APrice1", shop.APrice1),
                new XAttribute("AValue1", shop.AValue1),
                new XAttribute("APrice2", shop.APrice2),
                new XAttribute("AValue2", shop.AValue2),
                new XAttribute("APrice3", shop.APrice3),
                new XAttribute("AValue3", shop.AValue3),
                new XAttribute("BUnit", shop.BUnit),
                new XAttribute("BPrice1", shop.BPrice1),
                new XAttribute("BValue1", shop.BValue1),
                new XAttribute("BPrice2", shop.BPrice2),
                new XAttribute("BValue2", shop.BValue2),
                new XAttribute("BPrice3", shop.BPrice3),
                new XAttribute("BValue3", shop.BValue3),
                new XAttribute("CUnit", shop.CUnit),
                new XAttribute("CPrice1", shop.CPrice1),
                new XAttribute("CValue1", shop.CValue1),
                new XAttribute("CPrice2", shop.CPrice2),
                new XAttribute("CValue2", shop.CValue2),
                new XAttribute("CPrice3", shop.CPrice3),
                new XAttribute("CValue3", shop.CValue3),
                //IsCheap="false" 
                new XAttribute("IsCheap", shop.IsCheap),
                //LimitCount="-1" 
                new XAttribute("LimitCount", shop.LimitCount),
                //StartDate="2000-01-01T00:00:00" 
                new XAttribute("StartDate", shop.StartDate),
                //EndDate="2050-01-01T00:00:00"
                 new XAttribute("EndDate", shop.EndDate));

        }

        public static XElement CreateBallInfo(BallInfo b)
        {
            return new XElement("Item", new XAttribute("ID", b.ID),
               new XAttribute("Power", b.Power),
               new XAttribute("Radii", b.Radii),
               new XAttribute("FlyingPartical", b.FlyingPartical == null ? "" : b.FlyingPartical),
               new XAttribute("BombPartical", b.BombPartical == null ? "" : b.BombPartical),
               new XAttribute("Crater", b.Crater == null ? "" : b.Crater),
               new XAttribute("AttackResponse", b.AttackResponse),
               new XAttribute("IsSpin", b.IsSpin),
               new XAttribute("SpinV", b.SpinV),
               new XAttribute("SpinVA", b.SpinVA),
               new XAttribute("Amount", b.Amount),
               new XAttribute("Wind", b.Wind),
               new XAttribute("DragIndex", b.DragIndex),
               new XAttribute("Weight", b.Weight),
               new XAttribute("Shake", b.Shake),
               new XAttribute("ShootSound", b.ShootSound == null ? "" : b.ShootSound),
               new XAttribute("BombSound", b.BombSound == null ? "" : b.BombSound),
               new XAttribute("ActionType", b.ActionType),
               new XAttribute("Mass", b.Mass));
        }

        public static XElement CreateBallConfigInfo(BallConfigInfo b)
        {
            return new XElement("Item",
               new XAttribute("TemplateID", b.TemplateID),
               new XAttribute("Common", b.Common),
               new XAttribute("CommonAddWound", b.CommonAddWound),
               new XAttribute("CommonMultiBall", b.CommonMultiBall),
               new XAttribute("Special", b.Special),
               new XAttribute("SpecialII", b.SpecialII));
        }

        public static XElement CreateCategoryInfo(CategoryInfo info)
        {
            return new XElement("Item", new XAttribute("ID", info.ID),
               new XAttribute("Name", info.Name == null ? "" : info.Name),
               new XAttribute("Place", info.Place),
               new XAttribute("Remark", info.Remark == null ? "" : info.Remark));
        }

        public static XElement CreateUserLoginList(PlayerInfo info)
        {
               return new XElement("Item", new XAttribute("ID", info.ID),
               new XAttribute("UserName", info.UserName == null ? "" : info.UserName),
               new XAttribute("NickName", info.NickName == null ? "" : info.NickName),
               new XAttribute("typeVIP", info.typeVIP),
               new XAttribute("VIPLevel", info.VIPLevel),
               new XAttribute("Grade", info.Grade),
               new XAttribute("Repute", info.Repute),
               new XAttribute("Sex", info.Sex),
               new XAttribute("WinCount", info.Win),
               new XAttribute("TotalCount", info.Total),
               new XAttribute("ConsortiaName", info.ConsortiaName == null ? "" : info.ConsortiaName),
               new XAttribute("ChairmanID", 0),
               new XAttribute("Rename", info.Rename),   
               new XAttribute("ConsortiaRename", info.ConsortiaRename ? info.NickName == info.ChairmanName : info.ConsortiaRename),
               new XAttribute("EscapeCount", info.Escape),
               new XAttribute("IsFirst", info.IsFirst),
               new XAttribute("FightPower", info.FightPower),
               new XAttribute("LastDate", DateTime.Now));

        }

        /// <summary>
        /// 任务模板
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static XElement CreateQuestInfo(QuestInfo info)
        {
            return new XElement("Item", new XAttribute("ID", info.ID),
                new XAttribute("QuestID", info.QuestID),
                new XAttribute("Title", info.Title),
                new XAttribute("Detail", info.Detail),
                new XAttribute("Objective", info.Objective),
                new XAttribute("NeedMinLevel", info.NeedMinLevel),
                new XAttribute("NeedMaxLevel", info.NeedMaxLevel),
                new XAttribute("PreQuestID", info.PreQuestID),
                new XAttribute("NextQuestID", info.NextQuestID),
                new XAttribute("IsOther", info.IsOther),
                new XAttribute("CanRepeat", info.CanRepeat),
                new XAttribute("RepeatInterval", info.RepeatInterval),
                new XAttribute("RepeatMax", info.RepeatMax),
                new XAttribute("RewardGP", info.RewardGP),
                new XAttribute("RewardGold", info.RewardGold),
                new XAttribute("RewardBindMoney", info.RewardBindMoney),//RewardGiftToken
                new XAttribute("RewardOffer", info.RewardOffer),
                new XAttribute("RewardRiches", info.RewardRiches),
                new XAttribute("RewardBuffID", info.RewardBuffID),
                new XAttribute("RewardBuffDate", info.RewardBuffDate),
                new XAttribute("RewardMoney", info.RewardMoney),
                new XAttribute("Rands", info.Rands),
                new XAttribute("RandDouble", info.RandDouble),
                new XAttribute("TimeMode", info.TimeMode),
                new XAttribute("StartDate", info.StartDate),
                new XAttribute("EndDate", info.EndDate),
        //MapID="0" 
            new XAttribute("MapID", info.MapID),
        //AutoEquip="false" 
            new XAttribute("AutoEquip", info.AutoEquip),
        //RewardMedal="0" 
            new XAttribute("OneKeyFinishNeedMoney", info.OneKeyFinishNeedMoney),
        //Rank="" 
            new XAttribute("Rank", info.Rank == null ? "" : info.Rank),
        //StarLev="0" 
            new XAttribute("StarLev", info.StarLev),
        //NotMustCount="0"
            new XAttribute("NotMustCount", info.NotMustCount),
            //Level2NeedMoney="-1" 
            new XAttribute("Level2NeedMoney", info.Level2NeedMoney),
            //Level3NeedMoney="-1" 
            new XAttribute("Level3NeedMoney", info.Level3NeedMoney),
            //Level4NeedMoney="-1" 
            new XAttribute("Level4NeedMoney", info.Level4NeedMoney),
            //Level5NeedMoney="-1">
            new XAttribute("Level5NeedMoney", info.Level5NeedMoney)
            );
        }

        /// <summary>
        /// 任务完成条件
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static XElement CreateQuestCondiction(QuestConditionInfo info)
        {
            return new XElement("Item_Condiction", new XAttribute("QuestID", info.QuestID),
                new XAttribute("CondictionID", info.CondictionID),
                new XAttribute("CondictionTitle", info.CondictionTitle),
                new XAttribute("CondictionType", info.CondictionType),
                new XAttribute("Para1", info.Para1),
                new XAttribute("Para2", info.Para2),
            // isOpitional="false"
            new XAttribute("isOpitional", info.isOpitional));
        }

        /// <summary>
        /// 任务奖励物品
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static XElement CreateQuestGoods(QuestAwardInfo info)
        {
            return new XElement("Item_Good", new XAttribute("QuestID", info.QuestID),
                new XAttribute("RewardItemID", info.RewardItemID),
                new XAttribute("IsSelect", info.IsSelect),
                new XAttribute("RewardItemValid", info.RewardItemValid),
                new XAttribute("RewardItemCount1", info.RewardItemCount1),
                new XAttribute("RewardItemCount2", info.RewardItemCount2),
                new XAttribute("RewardItemCount3", info.RewardItemCount3),
                new XAttribute("RewardItemCount4", info.RewardItemCount4),
                new XAttribute("RewardItemCount5", info.RewardItemCount5),
                new XAttribute("StrengthenLevel", info.StrengthenLevel),
                new XAttribute("AttackCompose", info.AttackCompose),
                new XAttribute("DefendCompose", info.DefendCompose),
                new XAttribute("AgilityCompose", info.AgilityCompose),
                new XAttribute("LuckCompose", info.LuckCompose),
                new XAttribute("IsCount", info.IsCount),
                new XAttribute("IsBind", info.IsBind));
            //IsBind="true"
        }
        public static XElement CreateQuestDataInfo(QuestDataInfo info)
        {
            return new XElement("Item", new XAttribute("CompletedDate", info.CompletedDate),
                new XAttribute("IsComplete", info.IsComplete),
                new XAttribute("Condition1", info.Condition1),
                new XAttribute("Condition2", info.Condition2),
                new XAttribute("Condition3", info.Condition3),
                new XAttribute("Condition4", info.Condition4),
                new XAttribute("QuestID", info.QuestID),
                new XAttribute("UserID", info.UserID),
                new XAttribute("RepeatFinish", info.RepeatFinish));
        }
        public static XElement CreateQuestRate(QuestRateInfo info)
        {
            return new XElement("Rate", 
                new XAttribute("BindMoneyRate", info.BindMoneyRate),
                new XAttribute("ExpRate", info.ExpRate),
                new XAttribute("GoldRate", info.GoldRate),
                new XAttribute("ExploitRate", info.ExploitRate),
                new XAttribute("CanOneKeyFinishTime", info.CanOneKeyFinishTime));
        }
        public static XElement CreateMapServer(ServerMapInfo info)
        {
            return new XElement("Item", new XAttribute("ServerID", info.ServerID),
                new XAttribute("OpenMap", info.OpenMap),
                new XAttribute("IsSpecial", info.IsSpecial));
        }

        public static XElement CreateActiveInfo(ActiveInfo info)
        {
            return new XElement("Item", new XAttribute("ActiveID", info.ActiveID),
                new XAttribute("Description", info.Description == null ? "" : info.Description),
                new XAttribute("Content", info.Content == null ? "" : info.Content),
                new XAttribute("AwardContent", info.AwardContent == null ? "" : info.AwardContent),
                new XAttribute("HasKey", info.HasKey),
                new XAttribute("EndDate", info.EndDate == null ? "" : ((DateTime)info.EndDate).ToString("yyyy-MM-dd HH:mm:ss")),
                new XAttribute("IsOnly", info.IsOnly),
                new XAttribute("StartDate", string.IsNullOrEmpty(info.StartDate.ToString()) ? "" : info.StartDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new XAttribute("Title", info.Title == null ? "" : info.Title),
                new XAttribute("Type", info.Type),
                new XAttribute("ActionTimeContent", info.ActionTimeContent == null ? "" : info.ActionTimeContent),
            //IsAdvance="false" 
            new XAttribute("IsAdvance", info.IsAdvance),
            //GoodsExchangeTypes="" 
            new XAttribute("GoodsExchangeTypes", info.GoodsExchangeTypes == null ? "" : info.GoodsExchangeTypes),
            //GoodsExchangeNum="" 
            new XAttribute("GoodsExchangeNum", info.GoodsExchangeNum == null ? "" : info.GoodsExchangeNum),
            //limitType="" 
            new XAttribute("limitType", info.limitType == null ? "" : info.limitType),
            //limitValue=""
            new XAttribute("limitValue", info.limitValue == null ? "" : info.limitValue),
            new XAttribute("IsShow", info.IsShow));
        }

        public static XElement CreateAuctionInfo(AuctionInfo info)
        {
            return new XElement("Item", new XAttribute("AuctionID", info.AuctionID),
                new XAttribute("AuctioneerID", info.AuctioneerID),
                new XAttribute("AuctioneerName", info.AuctioneerName == null ? "" : info.AuctioneerName),
                new XAttribute("BeginDate", info.BeginDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new XAttribute("BuyerID", info.BuyerID),
                new XAttribute("BuyerName", info.BuyerName == null ? "" : info.BuyerName),
                new XAttribute("ItemID", info.ItemID),
                new XAttribute("Mouthful", info.Mouthful),
                new XAttribute("PayType", info.PayType),
                new XAttribute("Price", info.Price),
                new XAttribute("Rise", info.Rise),
                new XAttribute("ValidDate", info.ValidDate));
        }

        public static XElement CreateConsortiaInfo(ConsortiaInfo info)
        {
            return new XElement("Item", new XAttribute("ConsortiaID", info.ConsortiaID),
                new XAttribute("BuildDate", info.BuildDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new XAttribute("CelebCount", info.CelebCount),
                new XAttribute("ChairmanID", info.ChairmanID),
                new XAttribute("ChairmanName", info.ChairmanName == null ? "" : info.ChairmanName),
                new XAttribute("ChairmanTypeVIP", info.ChairmanTypeVIP),//ChairmanTypeVIP="0" 
                new XAttribute("ChairmanVIPLevel", info.ChairmanVIPLevel),//ChairmanVIPLevel="1"                 
                new XAttribute("ConsortiaName", info.ConsortiaName == null ? "" : info.ConsortiaName),
                new XAttribute("CreatorID", info.CreatorID),
                new XAttribute("CreatorName", info.CreatorName == null ? "" : info.CreatorName),
                new XAttribute("Description", info.Description == null ? "" : info.Description),
                new XAttribute("Honor", info.Honor),
                new XAttribute("IP", info.IP),
                new XAttribute("Level", info.Level),
                new XAttribute("MaxCount", info.MaxCount),
                new XAttribute("Placard", info.Placard == null ? "" : info.Placard),
                new XAttribute("Repute", info.Repute),
                new XAttribute("Count", info.Count),
                new XAttribute("Riches", info.Riches),
                new XAttribute("FightPower", info.FightPower),//FightPower="981928" 
                new XAttribute("DeductDate", info.DeductDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new XAttribute("AddDayHonor", info.AddDayHonor),
                new XAttribute("AddDayRiches", info.AddDayRiches),
                new XAttribute("AddWeekHonor", info.AddWeekHonor),
                new XAttribute("AddWeekRiches", info.AddWeekRiches),
                new XAttribute("LastDayRiches", info.LastDayRiches),
                new XAttribute("OpenApply", info.OpenApply),
                new XAttribute("StoreLevel", info.StoreLevel),
                new XAttribute("SmithLevel", info.SmithLevel),
                new XAttribute("ShopLevel", info.ShopLevel),
                new XAttribute("BufferLevel", info.SkillLevel),//info.SkillLevel
                new XAttribute("ConsortiaGiftGp", 940),//ConsortiaGiftGp="940" 
                new XAttribute("ConsortiaAddDayGiftGp", 0),//ConsortiaAddDayGiftGp="0" 
                new XAttribute("ConsortiaAddWeekGiftGp", 8),//ConsortiaAddWeekGiftGp="8" 
                new XAttribute("Port", info.Port),//);
                new XAttribute("IsVoting", false),//IsVoting="false" 
                new XAttribute("VoteRemainDay", 3),//VoteRemainDay="3" 
                new XAttribute("CharmGP", 940),//CharmGP="940" 
                new XAttribute("BadgeID", 0),//BadgeID="0" 
                new XAttribute("BadgeBuyTime", DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd HH:mm:ss")),//BadgeBuyTime="2012-08-16 16:30:30" 
                new XAttribute("ValidDate", 0));//ValidDate="0"

        }

        public static XElement CreateConsortiaApplyUserInfo(ConsortiaApplyUserInfo info)
        {
            return new XElement("Item", new XAttribute("ID", info.ID),
                new XAttribute("ApplyDate", info.ApplyDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new XAttribute("ConsortiaID", info.ConsortiaID),
                new XAttribute("ConsortiaName", info.ConsortiaName == null ? "" : info.ConsortiaName),
                new XAttribute("Remark", info.Remark),
                new XAttribute("UserID", info.UserID),
                new XAttribute("UserName", info.UserName == null ? "" : info.UserName),
                new XAttribute("UserLevel", info.UserLevel),
                new XAttribute("Win", info.Win),
                new XAttribute("Total", info.Total),
                new XAttribute("Repute", info.Repute));
        }

        public static XElement CreateConsortiaInviteUserInfo(ConsortiaInviteUserInfo info)
        {
            return new XElement("Item", new XAttribute("ID", info.ID),
                new XAttribute("CelebCount", info.CelebCount),
                new XAttribute("ChairmanName", info.ChairmanName == null ? "" : info.ChairmanName),
                new XAttribute("ConsortiaID", info.ConsortiaID),
                new XAttribute("ConsortiaName", info.ConsortiaName == null ? "" : info.ConsortiaName),
                new XAttribute("Count", info.Count),
                new XAttribute("Honor", info.Honor),
                new XAttribute("InviteDate", info.InviteDate),
                new XAttribute("InviteID", info.InviteID),
                new XAttribute("InviteName", info.InviteName == null ? "" : info.InviteName),
                new XAttribute("Remark", info.Remark == null ? "" : info.Remark),
                new XAttribute("Repute", info.Repute),
                new XAttribute("UserID", info.UserID),
                new XAttribute("UserName", info.UserName == null ? "" : info.UserName));
        }

        public static XElement CreateConsortiaUserInfo(ConsortiaUserInfo info)
        {
            return new XElement("Item", new XAttribute("ID", info.ID),
                new XAttribute("ConsortiaID", info.ConsortiaID),
                new XAttribute("DutyID", info.DutyID),
                new XAttribute("DutyName", info.DutyName == null ? "" : info.DutyName),
                new XAttribute("GP", info.GP),
                new XAttribute("Grade", info.Grade),
                new XAttribute("Right", info.Right),
                new XAttribute("DutyLevel", info.Level),
                new XAttribute("Offer", info.Offer),
                new XAttribute("RatifierID", info.RatifierID),
                new XAttribute("RatifierName", info.RatifierName == null ? "" : info.RatifierName),
                new XAttribute("Remark", info.Remark == null ? "" : info.Remark),
                new XAttribute("Repute", info.Repute),
                new XAttribute("State", info.State == 1 ? 1 : 0),
                new XAttribute("UserID", info.UserID),
                new XAttribute("Hide", info.Hide),
                new XAttribute("Colors", info.Colors == null ? "" : info.Colors),
                new XAttribute("Skin", info.Skin == null ? "" : info.Skin),
                new XAttribute("Style", info.Style),
                new XAttribute("LastDate", info.LastDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new XAttribute("Sex", info.Sex),
                new XAttribute("IsBanChat", info.IsBanChat),
                new XAttribute("WinCount", info.Win),
                new XAttribute("TotalCount", info.Total),
                new XAttribute("EscapeCount", info.Escape),
                new XAttribute("RichesOffer", info.RichesOffer),
                new XAttribute("RichesRob", info.RichesRob),
                new XAttribute("Nimbus", info.Nimbus),
                new XAttribute("LoginName", info.LoginName == null ? "" : info.LoginName),
                new XAttribute("UserName", info.UserName == null ? "" : info.UserName),
                new XAttribute("typeVIP", info.typeVIP),
                new XAttribute("VIPLevel", info.VIPLevel),
                new XAttribute("AchievementPoint", 1),
                new XAttribute("Rank", "honor"),
                new XAttribute("FightPower", info.FightPower),
                new XAttribute("IsCandidate", false),
                //new XAttribute("IsDownGrade", true),
                //new XAttribute("IsEditorPlacard", true),
                //new XAttribute("IsEditorDescription", true),
                //new XAttribute("IsExpel", true),
                //new XAttribute("IsEditorUser", true),
                new XAttribute("IsVote", false),
                new XAttribute("TotalRichesOffer", 106),
                new XAttribute("LastWeekRichesOffer", 0));                
                //new XAttribute("IsRatify", true),
                //new XAttribute("IsChat", true));



        }

        public static XElement CreateConsortiaIMInfo(ConsortiaUserInfo info)
        {
            return new XElement("Item", new XAttribute("ID", info.ID),
                      new XAttribute("ConsortiaID", info.ConsortiaID),
                      new XAttribute("DutyID", info.DutyID),
                      new XAttribute("DutyName", info.DutyName == null ? "" : info.DutyName),
                      new XAttribute("GP", info.GP),
                      new XAttribute("Grade", info.Grade),
                      new XAttribute("Level", info.Level),
                      new XAttribute("Offer", info.Offer),
                      new XAttribute("Remark", info.Remark == null ? "" : info.Remark),
                      new XAttribute("Repute", info.Repute),
                      new XAttribute("State", info.State == 1 ? 1 : 0),
                      new XAttribute("UserID", info.UserID),
                      new XAttribute("Hide", info.Hide),
                      new XAttribute("Colors", info.Colors == null ? "" : info.Colors),
                      new XAttribute("Skin", info.Skin == null ? "" : info.Skin),
                      new XAttribute("Style", info.Style),
                      new XAttribute("LastDate", info.LastDate.ToString("yyyy-MM-dd HH:mm:ss")),
                      new XAttribute("Sex", info.Sex),
                      new XAttribute("LoginName", info.LoginName),
                      new XAttribute("NickName", info.UserName == null ? "" : info.UserName));
        }

        public static XElement CreateConsortiaDutyInfo(ConsortiaDutyInfo info)
        {
            return new XElement("Item", new XAttribute("DutyID", info.DutyID),
                new XAttribute("ConsortiaID", info.ConsortiaID),
                new XAttribute("DutyName", info.DutyName == null ? "" : info.DutyName),
                new XAttribute("Right", info.Right),
                new XAttribute("Level", info.Level));
        }

        public static XElement CreateConsortiaApplyAllyInfo(ConsortiaApplyAllyInfo info)
        {
            return new XElement("Item", new XAttribute("ID", info.ID),
                new XAttribute("CelebCount", info.CelebCount),
                new XAttribute("ChairmanName", info.ChairmanName == null ? "" : info.ChairmanName),
                new XAttribute("ConsortiaID", info.Consortia1ID),
                //new XAttribute("Consortia2ID", info.Consortia2ID),
                new XAttribute("ConsortiaName", info.ConsortiaName == null ? "" : info.ConsortiaName),
                new XAttribute("Count", info.Count),
                new XAttribute("Date", info.Date.ToString("yyyy-MM-dd HH:mm:ss")),
                new XAttribute("Honor", info.Honor),
                new XAttribute("Remark", info.Remark == null ? "" : info.Remark),
                new XAttribute("Level", info.Level),
                new XAttribute("Description", info.Description == null ? "" : info.Description),
                new XAttribute("Repute", info.Repute));
        }

        public static XElement CreateConsortiaAllyInfo(ConsortiaAllyInfo info)
        {
            return new XElement("Item", new XAttribute("ID", info.ID),
                //new XAttribute("CelebCount", info.CelebCount),
                new XAttribute("ChairmanName", info.ChairmanName1 == null ? "" : info.ChairmanName1),
                new XAttribute("ConsortiaID", info.Consortia1ID),
                //new XAttribute("Consortia2ID", info.Consortia2ID),
                new XAttribute("ConsortiaName", info.ConsortiaName1 == null ? "" : info.ConsortiaName1),
                new XAttribute("Count", info.Count1),
                new XAttribute("Honor", info.Honor1),
                new XAttribute("State", info.State),
                new XAttribute("Date", info.Date.ToString("yyyy-MM-dd HH:mm:ss")),
                new XAttribute("Level", info.Level1),
                new XAttribute("IsApply", info.IsApply),
                new XAttribute("Description", info.Description1),
                new XAttribute("Riches", info.Riches1),
                new XAttribute("Repute", info.Repute1));
        }

        public static XElement CreateConsortiaEventInfo(ConsortiaEventInfo info)
        {
            return new XElement("Item", new XAttribute("ID", info.ID),
                new XAttribute("ConsortiaID", info.ConsortiaID),
                new XAttribute("Date", info.Date.ToString("yyyy-MM-dd HH:mm:ss")),
                new XAttribute("Type", info.Type),
                new XAttribute("Remark", info.Remark == null ? "" : info.Date.ToString("yy-MM-dd") + " " + info.Remark));
        }

        public static XElement CreateConsortiLevelInfo(ConsortiaLevelInfo info)
        {
            return new XElement("Item", new XAttribute("Level", info.Level),
                new XAttribute("Count", info.Count),
                new XAttribute("Deduct", info.Deduct),
                new XAttribute("NeedGold", info.NeedGold),
                new XAttribute("NeedItem", info.NeedItem),
                new XAttribute("Reward", info.Reward),
                new XAttribute("ShopRiches", info.ShopRiches),
                new XAttribute("SmithRiches", info.SmithRiches),
                new XAttribute("StoreRiches", info.StoreRiches),
                new XAttribute("Riches", info.Riches),
            //BufferRiches="10000"
            new XAttribute("BufferRiches", info.BufferRiches));
        }

        public static XElement CreateCelebInfo(PlayerInfo info)
        {
            return new XElement("Item", new XAttribute("ID", info.ID),
                new XAttribute("UserName", info.UserName == null ? "" : info.UserName),
                new XAttribute("NickName", info.NickName == null ? "" : info.NickName),
                new XAttribute("Grade", info.Grade),
                new XAttribute("Colors", info.Colors == null ? "" : info.Colors),
                new XAttribute("Skin", info.Skin == null ? "" : info.Skin),
                new XAttribute("Sex", info.Sex),
                new XAttribute("Style", info.Style == null ? "" : info.Style),
                new XAttribute("ConsortiaName", info.ConsortiaName == null ? "" : info.ConsortiaName),
                new XAttribute("Hide", info.Hide),
                new XAttribute("Offer", info.Offer),
                new XAttribute("ReputeOffer", info.ReputeOffer),
                new XAttribute("ConsortiaHonor", info.ConsortiaHonor),
                new XAttribute("ConsortiaLevel", info.ConsortiaLevel),
                new XAttribute("StoreLevel", info.StoreLevel),
                new XAttribute("ShopLevel", info.ShopLevel),
                new XAttribute("SmithLevel", info.SmithLevel),
                new XAttribute("ConsortiaRepute", info.ConsortiaRepute),
                new XAttribute("WinCount", info.Win),
                new XAttribute("TotalCount", info.Total),
                new XAttribute("EscapeCount", info.Escape),
                new XAttribute("Repute", info.Repute),
                new XAttribute("AddDayGP", info.AddDayGP),
                new XAttribute("AddDayOffer", info.AddDayOffer),
                new XAttribute("AddWeekGP", info.AddWeekGP),
                new XAttribute("AddWeekOffer", info.AddWeekOffer),
                new XAttribute("ConsortiaRiches", info.ConsortiaRiches),
                new XAttribute("Nimbus", info.Nimbus),
                new XAttribute("GP", info.GP),
                new XAttribute("FightPower", info.FightPower));

        }

        public static XElement CreateBestEquipInfo(BestEquipInfo info)
        {
            return new XElement("Item", new XAttribute("Date", info.Date.ToString("yyyy-MM-dd HH:mm:ss")),
                new XAttribute("GP", info.GP),
                new XAttribute("Grade", info.Grade),
                new XAttribute("ItemName", info.ItemName == null ? "" : info.ItemName),
                new XAttribute("NickName", info.NickName == null ? "" : info.NickName),
                new XAttribute("Sex", info.Sex),
                new XAttribute("Strengthenlevel", info.Strengthenlevel),
                new XAttribute("Type", info.UserName == null ? "" : info.UserName));
        }

        public static XElement CreateMailInfo(MailInfo info, string nodeName)
        {
            TimeSpan days = DateTime.Now.Subtract(info.SendTime);
            return new XElement(nodeName, new XAttribute("ID", info.ID),
                new XAttribute("Title", info.Title),
                new XAttribute("Content", info.Content),
                new XAttribute("Sender", info.Sender),
                new XAttribute("Receiver", info.Receiver),
                new XAttribute("SendTime", info.SendTime.ToString("yyyy-MM-dd HH:mm:ss")),
                //new XAttribute("Days", info.ValidDate - days.Hours),
                new XAttribute("ValidDate", info.ValidDate),
                new XAttribute("Gold", info.Gold),
                new XAttribute("Money", info.Money),
                new XAttribute("Annex1ID", info.Annex1 == null ? "" : info.Annex1),
                new XAttribute("Annex2ID", info.Annex2 == null ? "" : info.Annex2),
                new XAttribute("Annex3ID", info.Annex3 == null ? "" : info.Annex3),
                new XAttribute("Annex4ID", info.Annex4 == null ? "" : info.Annex4),
                new XAttribute("Annex5ID", info.Annex5 == null ? "" : info.Annex5),
                new XAttribute("Annex1Name", info.Annex1Name == null ? "" : info.Annex1Name),
                new XAttribute("Annex2Name", info.Annex2Name == null ? "" : info.Annex2Name),
                new XAttribute("Annex3Name", info.Annex3Name == null ? "" : info.Annex3Name),
                new XAttribute("Annex4Name", info.Annex4Name == null ? "" : info.Annex4Name),
                new XAttribute("Annex5Name", info.Annex5Name == null ? "" : info.Annex5Name),
                new XAttribute("AnnexRemark", info.AnnexRemark == null ? "" : info.AnnexRemark),
                new XAttribute("Type", info.Type),
                //new XAttribute("ValidDate", info.ValidDate),
                new XAttribute("IsRead", info.IsRead));
        }

        public static XElement CreateBuffInfo(BufferInfo info)
        {
            return new XElement("Item", new XAttribute("BeginDate", info.BeginDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new XAttribute("Data", info.Data == null ? "" : info.Data),
                new XAttribute("IsExist", info.IsExist),
                new XAttribute("Type", info.Type),
                new XAttribute("UserID", info.UserID),
                new XAttribute("ValidDate", info.ValidDate),
                new XAttribute("Value", info.Value));
        }

        public static XElement CreateMarryInfo(MarryInfo info)
        {
            return new XElement("Info", new XAttribute("ID", info.ID),
                new XAttribute("UserID", info.UserID),
                new XAttribute("IsPublishEquip", info.IsPublishEquip),
                new XAttribute("Introduction", info.Introduction),
                new XAttribute("NickName", info.NickName),
                new XAttribute("IsConsortia", info.IsConsortia),
                new XAttribute("ConsortiaID", info.ConsortiaID),
                new XAttribute("Sex", info.Sex),
                new XAttribute("Win", info.Win),
                new XAttribute("Total", info.Total),
                new XAttribute("Escape", info.Escape),
                new XAttribute("GP", info.GP),
                new XAttribute("Honor", info.Honor),
                new XAttribute("Style", info.Style),
                new XAttribute("Colors", info.Colors),
                new XAttribute("Hide", info.Hide),
                new XAttribute("Grade", info.Grade),
                new XAttribute("State", info.State),
                new XAttribute("Repute", info.Repute),
                new XAttribute("Skin", info.Skin),
                new XAttribute("Offer", info.Offer),
                new XAttribute("IsMarried", info.IsMarried),
                new XAttribute("ConsortiaName", info.ConsortiaName),
                new XAttribute("DutyName", info.DutyName),
                new XAttribute("Nimbus", info.Nimbus),
                new XAttribute("FightPower", info.FightPower));
        }

        public static XElement CreateActiveInfo(DailyAwardInfo info)
        {
            return new XElement("Item", new XAttribute("ID", info.ID),
                new XAttribute("Count", info.Count),
                new XAttribute("CountRemark", info.CountRemark == null ? "" : info.CountRemark),
                new XAttribute("IsBinds", info.IsBinds),
                new XAttribute("Remark", info.Remark == null ? "" : info.Remark),
                new XAttribute("Sex", info.Sex),
                new XAttribute("TemplateID", info.TemplateID),
                new XAttribute("Type", info.Type),
                new XAttribute("ValidDate", info.ValidDate),
            // GetWay="0" 
            new XAttribute("GetWay", info.GetWay),
            //AwardDays="2"
            new XAttribute("AwardDays", info.AwardDays));
        }

        public static XElement CreateConsortiaEquipControlInfo(ConsortiaEquipControlInfo info)
        {
            return new XElement("Item", new XAttribute("ConsortiaID", info.ConsortiaID),
                new XAttribute("Level", info.Level),
                new XAttribute("Riches", info.Riches),
                new XAttribute("Type", info.Type));
        }

        public static XElement CreatNPCInfo(NpcInfo info)
        {
            return new XElement("Item", new XAttribute("ID", info.ID),
                new XAttribute("Name", info.Name),
                new XAttribute("Level", info.Level),
                new XAttribute("Camp", info.Camp),
                new XAttribute("Type", info.Type),
                new XAttribute("Blood", info.Blood),
                new XAttribute("MoveMin", info.MoveMin),
                new XAttribute("MoveMax", info.MoveMax),
                new XAttribute("BaseDamage", info.BaseDamage),
                new XAttribute("BaseGuard", info.BaseGuard),
                new XAttribute("Defence", info.Defence),
                new XAttribute("Agility", info.Agility),
                new XAttribute("Lucky", info.Lucky),
                new XAttribute("ModelID", info.ModelID),
                new XAttribute("ResourcesPath", info.ResourcesPath),
                new XAttribute("DropRate", info.DropRate),
                new XAttribute("Experience", info.Experience),
                new XAttribute("Delay", info.Delay),
                new XAttribute("Immunity", info.Immunity),
                new XAttribute("Alert", info.Alert),
                new XAttribute("Range", info.Range),
                new XAttribute("Preserve", info.Preserve),
                new XAttribute("Script", info.Script),
                new XAttribute("FireX", info.FireX),
                new XAttribute("FireY", info.FireY),
                new XAttribute("DropId", info.DropId));

        }
    }
}
