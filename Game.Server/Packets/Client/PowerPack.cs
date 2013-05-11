using Bussiness;
using Bussiness.Managers;
using Game.Base.Packets;
using Game.Server;
using Game.Server.GameUtils;
using Game.Server.Managers;
using Game.Server.Packets;
using SqlDataProvider.Data;
using System;
using System.Collections.Generic;

namespace Game.Server.Packets.Client
{

    [PacketHandler((byte)ePackageType.BUY_GIFTBAG, "购买物品")]
    public class PowerPack : IPacketHandler
    {
        private SqlDataProvider.Data.ItemInfo getitem(GameClient client, int type, string color, string skin, int id)
        {
            ConsortiaInfo info = ConsortiaMgr.FindConsortiaInfo(client.Player.PlayerCharacter.ConsortiaID);
            ShopItemInfo shopItemInfoById = ShopMgr.GetShopItemInfoById(id);
            bool flag = false;
            SqlDataProvider.Data.ItemInfo info3 = null;
            if (shopItemInfoById != null)
            {
                info3 = SqlDataProvider.Data.ItemInfo.CreateFromTemplate(ItemMgr.FindItemTemplate(shopItemInfoById.TemplateID), 1, 102);
                if (0 == shopItemInfoById.BuyType)
                {
                    if (1 == type)
                    {
                        info3.ValidDate = shopItemInfoById.AUnit;
                    }
                    if (2 == type)
                    {
                        info3.ValidDate = shopItemInfoById.BUnit;
                    }
                    if (3 == type)
                    {
                        info3.ValidDate = shopItemInfoById.CUnit;
                    }
                }
                else
                {
                    if (1 == type)
                    {
                        info3.Count = shopItemInfoById.AUnit;
                    }
                    if (2 == type)
                    {
                        info3.Count = shopItemInfoById.BUnit;
                    }
                    if (3 == type)
                    {
                        info3.Count = shopItemInfoById.CUnit;
                    }
                }
                if ((info3 != null) || (shopItemInfoById != null))
                {
                    info3.Color = (color == null) ? "" : color;
                    info3.Skin = (skin == null) ? "" : skin;
                    if (flag)
                    {
                        info3.IsBinds = true;
                        return info3;
                    }
                    info3.IsBinds = Convert.ToBoolean(shopItemInfoById.IsBind);
                }
            }
            return info3;
        }

        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            if (998 <= client.Player.PlayerCharacter.Money)
            {
                SqlDataProvider.Data.ItemInfo item = this.getitem(client, 1, "", "", 1101801);
                SqlDataProvider.Data.ItemInfo info2 = this.getitem(client, 1, "", "", 1102001);
                SqlDataProvider.Data.ItemInfo info3 = this.getitem(client, 1, "", "", 1102401);
                SqlDataProvider.Data.ItemInfo info4 = this.getitem(client, 1, "", "", 1102401);
                SqlDataProvider.Data.ItemInfo info5 = this.getitem(client, 1, "", "", 1102401);
                PlayerInventory bag = client.Player.GetInventory(eBageType.Store);
                PlayerInventory inventory = client.Player.GetInventory(eBageType.PropBag);
                List<SqlDataProvider.Data.ItemInfo> items = bag.GetItems();
                foreach (SqlDataProvider.Data.ItemInfo info6 in items)
                {
                    bag.MoveToStore(bag, info6.Place, inventory.FindFirstEmptySlot(0), inventory, 50);
                }
                bag.AddItemTo(item, 4);
                bag.AddItemTo(info2, 3);
                bag.AddItemTo(info3, 2);
                bag.AddItemTo(info4, 1);
                bag.AddItemTo(info5, 0);
                client.Out.SendMessage(eMessageType.ERROR, LanguageMgr.GetTranslation("UserBuyItemHandler.Success", new object[0]));
                client.Player.RemoveMoney(998);
            }
            else
            {
                client.Out.SendMessage(eMessageType.ERROR, LanguageMgr.GetTranslation("UserBuyItemHandler.NoMoney", new object[0]));
            }
            return 0;
        }
    }
}

