using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base.Packets;
using Bussiness;
using SqlDataProvider.Data;
using Game.Server.GameUtils;
using Game.Server.Managers;
using log4net;
using System.Reflection;

namespace Game.Server.Packets.Client
{
    [PacketHandler((byte)ePackageType.CHANGE_PLACE_ITEM, "改变物品位置")]
    public class UserChangeItemPlaceHandler:IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            eBageType bageType = (eBageType)packet.ReadByte();
            int slot = packet.ReadInt();
            eBageType type2 = (eBageType)packet.ReadByte();
            int toSlot = packet.ReadInt();
            int count = packet.ReadInt();
            PlayerInventory bag = client.Player.GetInventory(bageType);
            PlayerInventory inventory = client.Player.GetInventory(type2);
            if (toSlot < 0)
            {
                if ((inventory.BagType == 11) || (inventory.BagType == 1))
                {
                    toSlot = inventory.FindFirstEmptySlot(0);
                }
                else
                {
                    toSlot = inventory.FindFirstEmptySlot(0x1f);
                }
            }
            if (count > 0)
            {
                if ((bag == null) || (bag.GetItemAt(slot) == null))
                {
                    return 0;
                }
                if ((bageType == type2) && (slot != -1))
                {
                    if (bag.GetItemAt(slot).Count >= count)
                    {
                        bag.MoveItem(slot, toSlot, count);
                    }
                    else
                    {
                        Console.WriteLine("--" + client.Player.Account + " Hack = CE");
                        bag.MoveItem(slot, toSlot, bag.GetItemAt(slot).Count);
                    }
                    return 1;
                }
                if ((slot == -1) && (toSlot != -1))
                {
                    bag.RemoveItemAt(toSlot);
                    return 1;
                }
                if (((((slot != -1) && (toSlot == -1)) && ((bageType != eBageType.CaddyBag) && (type2 != eBageType.Bank))) && (bageType != eBageType.Store)) && (type2 != eBageType.Store))
                {
                    if (bageType == eBageType.MainBag)
                    {
                        bag.AddItem(client.Player.GetItemAt(bageType, slot), 0x1f);
                    }
                    else
                    {
                        bag.AddItem(client.Player.GetItemAt(bageType, slot), 0);
                    }
                    return 1;
                }
                if ((((slot != -1) && (type2 != bageType)) && (type2 != eBageType.Store)) && (type2 != eBageType.MainBag))
                {
                    ConsortiaInfo info = ConsortiaMgr.FindConsortiaInfo(client.Player.PlayerCharacter.ConsortiaID);
                    if (info != null)
                    {
                        if (type2 == eBageType.Store)
                        {
                        }
                        bag.MoveToStore(bag, slot, toSlot, inventory, info.StoreLevel * 10);
                        return 1;
                    }
                }
                if ((type2 == eBageType.Store) || (bageType == eBageType.Store))
                {
                    SqlDataProvider.Data.ItemInfo itemAt = client.Player.GetItemAt(bageType, slot);
                    if ((itemAt != null) && (itemAt.Count > 1))
                    {
                        itemAt.Count--;
                        bag.UpdateItem(itemAt);
                        SqlDataProvider.Data.ItemInfo item = itemAt.Clone();
                        item.Count = 1;
                        if (inventory.GetItemAt(toSlot) == null)
                        {
                            inventory.AddItemTo(item, toSlot);
                        }
                        else
                        {
                            SqlDataProvider.Data.ItemInfo itemByTemplateID = bag.GetItemByTemplateID(0, inventory.GetItemAt(toSlot).TemplateID);
                            if (itemByTemplateID == null)
                            {
                                bag.MoveToStore(inventory, toSlot, bag.FindFirstEmptySlot(0), bag, 0x3e7);
                            }
                            else
                            {
                                itemByTemplateID.Count++;
                                bag.UpdateItem(itemByTemplateID);
                                inventory.RemoveItemAt(toSlot);
                            }
                            inventory.AddItemTo(item, toSlot);
                        }
                    }
                    else
                    {
                        if ((((type2 != eBageType.Store) && (type2 != eBageType.MainBag)) && ((bag.GetItemAt(slot) != null) && (bag.GetItemAt(slot).Template.CategoryID == 7))) && (((toSlot > 0) && (toSlot < 0x1f)) && (toSlot != 6)))
                        {
                            return 1;
                        }
                        try
                        {
                            bag.MoveToStore(bag, slot, toSlot, inventory, 50);
                        }
                        catch (Exception)
                        {
                            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType).InfoFormat("ERROR USER CHANGEITEM placce: {0},toplace: {1},bagType: {2},ToBagType {3}", new object[] { slot, toSlot, bageType, type2 });
                            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType).InfoFormat("item :{0}, toitem {1}", bag.GetItemAt(slot), inventory.GetItemAt(toSlot));
                        }
                    }
                    return 1;
                }
                if ((type2 == eBageType.MainBag) && (bageType == eBageType.Bank))
                {
                    bag.MoveToStore(bag, slot, toSlot, inventory, 50);
                    return 1;
                }
            }
            return 0;
        }
    }
}
