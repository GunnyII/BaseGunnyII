using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Server.GameObjects;
using Game.Base.Packets;
using Bussiness;
using Bussiness.Managers;
using SqlDataProvider.Data;
using Game.Server.Managers;
using Game.Server.GameUtils;
using System.Configuration;
using Game.Server.Statics;


namespace Game.Server.Packets.Client
{
    
    [PacketHandler((byte)ePackageType.ITEM_FUSION, "熔化")]
    public class ItemFusionHandler : IPacketHandler
    {
        /// <summary>
        /// 
        /// </summary>
        public static List<int> FusionFormulID = new List<int> { 11201, 11202, 11203, 11204, 11301, 11302, 11303, 11304 };
        public double Zomm = 0.75;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="packet"></param>
        /// <returns></returns>
        
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
         
            //int i;
            //eBageType bagType;
            //int place;
            //ItemInfo info;
            bool isBind;
            int mustGold;
            bool result;
            ItemTemplateInfo rewardItem;
            StringBuilder str = new StringBuilder();
            int opertionType = packet.ReadByte();
            //int count = packet.ReadInt();
            int MinValid = int.MaxValue;
            List<ItemInfo> items = new List<ItemInfo>();
            List<ItemInfo> appendItems = new List<ItemInfo>();
            List<eBageType> bagTypes = new List<eBageType>();

            if (client.Player.PlayerCharacter.HasBagPassword && client.Player.PlayerCharacter.IsLocked)
            {
                client.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("Bag.Locked", new object[0]));
                return 1;
            }
           
            for (int i = 0; i < 4; i++)
            {
                //bagType = (eBageType) packet.ReadByte();
                //place = packet.ReadInt();
                ItemInfo info = client.Player.GetItemAt(eBageType.Store, i);
                if (info != null)
                {
                    if (items.Contains(info))
                    {
                        client.Out.SendMessage(eMessageType.Normal, "Bad Input 1");
                        return 1;
                    }
                    str.Append(string.Concat(info.ItemID, ":", info.TemplateID, "," ));
                    items.Add(info);
                    bagTypes.Add(eBageType.Store);
                    if ((info.ValidDate < MinValid) && (info.ValidDate != 0))
                    {
                        MinValid = info.ValidDate;
                    }
                }
            }
         
            if (MinValid == int.MaxValue)
            {
                MinValid = 0;
                items.Clear();
            }
            PlayerInventory storeBag2 = client.Player.StoreBag2;
            ItemInfo formul = null;// storeBag2.GetItemAt(0);
            ItemInfo tempitem = null;
            string beginProperty = null;
            string AddItem = "";

            for (int i = 1; i <= 4; i++)
            {
                items.Add(storeBag2.GetItemAt(i));
            }
            /*
            using (ItemRecordBussiness db = new ItemRecordBussiness())
            {
                foreach (ItemInfo item in items)
                {
                    db.FusionItem(item, ref beginProperty);
                }
            }
            */
            /*/int appendCount = packet.ReadInt();
            List<eBageType> bagTypesAppend = new List<eBageType>();
            for (int i = 0; i < 4; i++)
            {
                //bagType = (eBageType) packet.ReadByte();
                //place = packet.ReadInt();
                ItemInfo info = client.Player.GetItemAt(eBageType.Store, i);
                if (info != null)
                {
                    if (items.Contains(info) || appendItems.Contains(info))
                    {
                        client.Out.SendMessage(eMessageType.Normal, "Bad Input 2");
                        return 1;
                    }
                    str.Append(string.Concat(info.ItemID, ":", info.TemplateID, "," ));
                    appendItems.Add(info);
                    bagTypesAppend.Add(eBageType.Store);
                    object objItem = AddItem;
                    AddItem = string.Concat(objItem, info.ItemID, ":", info.Template.Name, ",", info.IsBinds, "|");
                }
            }
            */
            if (opertionType != 0)
            {
                storeBag2.ClearBag();
                mustGold = 500;// (count + appendCount) * 400;
                if (client.Player.PlayerCharacter.Gold < mustGold)
                {
                    client.Out.SendMessage(eMessageType.ERROR, LanguageMgr.GetTranslation("ItemFusionHandler.NoMoney"));
                    return 0;
                }
                isBind = false;
                result = false;
                rewardItem = null;
                foreach (int formulID in FusionFormulID)
                {
                    formul = ItemInfo.CreateFromTemplate(ItemMgr.FindItemTemplate(formulID), 1, 105);
                    rewardItem = FusionMgr.Fusion(items, formul, ref isBind, ref result);// appendItems,
                    if (rewardItem != null)
                    {
                        break;
                    }
                }
            }
            else
            {
                isBind = false;
                Dictionary<int, double> previewItemList = null;
                foreach (int formulID in FusionFormulID)
                {
                    formul = ItemInfo.CreateFromTemplate(ItemMgr.FindItemTemplate(formulID), 1, 105);
                    previewItemList = FusionMgr.FusionPreview(items, formul, ref isBind);// appendItems,
                    if ((previewItemList != null) && (previewItemList.Count > 0))
                    {
                        break;
                    }
                }
                if ((previewItemList != null) && (previewItemList.Count > 0))
                {
                    if (previewItemList.Count != 0)
                    {
                        client.Out.SendFusionPreview(client.Player, previewItemList, isBind, MinValid);
                    }
                }
                else
                {
                    client.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("ItemFusionHandler.ItemNotEnough"));
                }
                return 0;
            }
            if (rewardItem != null)
            {
                client.Player.RemoveGold(mustGold);
                for (int i = 0; i < items.Count; i++)
                {
                    ItemInfo local1 = items[i];
                    local1.Count--;
                    client.Player.UpdateItem(items[i]);
                }
                formul.Count--;
                client.Player.UpdateItem(formul);
                for (int i = 0; i < appendItems.Count; i++)
                {
                    ItemInfo local2 = appendItems[i];
                    local2.Count--;
                    client.Player.UpdateItem(appendItems[i]);
                }

                if (result)
                {
                    str.Append(rewardItem.TemplateID + ",");
                    ItemInfo item = ItemInfo.CreateFromTemplate(rewardItem, 1, 105);
                    if (item == null)
                    {
                        return 0;
                    }
                    tempitem = item;
                    item.IsBinds = isBind;
                    item.ValidDate = MinValid;
                    client.Player.OnItemFusion(item.Template.FusionType);
                    client.Out.SendFusionResult(client.Player, result);
                    client.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("ItemFusionHandler.Succeed1") + item.Template.Name);
                    
                    if (((item.TemplateID >= 8300) && (item.TemplateID <= 8999)) || ((item.TemplateID >= 9300) && (item.TemplateID <= 9999)) || ((item.TemplateID >= 14300) && (item.TemplateID <= 14999)) || ((item.TemplateID >= 7024) && (item.TemplateID <= 7028)) || ((item.TemplateID >= 14006) && (item.TemplateID <= 14010)) || ((item.TemplateID >= 17000) && (item.TemplateID <= 17010)))
                    {
                        string msg = LanguageMgr.GetTranslation("ItemFusionHandler.Notice", client.Player.PlayerCharacter.NickName, item.Template.Name);
                        GSPacketIn pkg1 = new GSPacketIn((byte)ePackageType.SYS_NOTICE);
                        pkg1.WriteInt(1);
                        pkg1.WriteString(msg);
                        GameServer.Instance.LoginServer.SendPacket(pkg1);
                        GamePlayer[] players = WorldMgr.GetAllPlayers();
                        foreach (GamePlayer p in players)
                        {
                            p.Out.SendTCP(pkg1);
                        }
                    }
                    if (!client.Player.AddTemplate(item, item.Template.BagType, item.Count))
                    {
                        str.Append("NoPlace");
                        client.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation(item.GetBagName()) + LanguageMgr.GetTranslation("ItemFusionHandler.NoPlace"));
                    }
                }
                else
                {
                    str.Append("false");
                    client.Out.SendFusionResult(client.Player, result);
                    client.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("ItemFusionHandler.Failed"));
                }
                LogMgr.LogItemAdd(client.Player.PlayerCharacter.ID, LogItemType.Fusion, beginProperty, tempitem, AddItem, Convert.ToInt32(result));
                client.Player.SaveIntoDatabase();
            }
            else
            {
                client.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("ItemFusionHandler.NoCondition"));
            }
            return 0;
        }
    
    }
}
