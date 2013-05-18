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
    [PacketHandler((byte)ePackageType.CHANGE_PLACE_GOODS, "改变物品位置")]
    public class UserChangeItemPlaceHandler:IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            eBageType bageType = (eBageType)packet.ReadByte();
            int slot = packet.ReadInt();
            eBageType toBageType = (eBageType)packet.ReadByte();
            int toSlot = packet.ReadInt();
            int count = packet.ReadInt();
            PlayerInventory bag = client.Player.GetInventory(bageType);
            PlayerInventory inventory = client.Player.GetInventory(toBageType);
            if (toSlot < 0)
            {
                if ((inventory.BagType == (int)eBageType.Bank) || (inventory.BagType == (int)eBageType.PropBag))
                {
                    toSlot = inventory.FindFirstEmptySlot(0);
                }
                else
                {
                    toSlot = inventory.FindFirstEmptySlot(31);
                }
            }  
            //==================================
            if (count > 0)
            {
                if ((bag == null) || (bag.GetItemAt(slot) == null))
                {
                    return 0;
                }
                if ((bageType == toBageType) && (slot != -1))
                {
                    if (bag.GetItemAt(slot).Count >= count)
                    {
                        Console.WriteLine("-----Stage 1 !");
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
                    Console.WriteLine("-----Stage 2 !");
                    bag.RemoveItemAt(toSlot);                   
                    return 1;
                }

                if (((((slot != -1) && (toSlot == -1)) && 
                    ((bageType != eBageType.CaddyBag) && 
                    (toBageType != eBageType.Bank))) && 
                    (bageType != eBageType.Store)) && 
                    (toBageType != eBageType.Store))
                {
                    if (bageType == eBageType.MainBag)
                    {
                        Console.WriteLine("-----Stage 3 !");
                        bag.AddItem(client.Player.GetItemAt(bageType, slot), 31);

                    }
                    else
                    {
                        Console.WriteLine("-----Stage 4 !");
                        //bag.AddItem(client.Player.GetItemAt(bageType, slot), 0);
                        client.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("Bag.Full"));
                    }
                    return 1;
                }

                if ((((slot != -1) && (toBageType != bageType)) && (toBageType != eBageType.Store)) && (toBageType != eBageType.MainBag))
                {
                    ConsortiaInfo info = ConsortiaMgr.FindConsortiaInfo(client.Player.PlayerCharacter.ConsortiaID);
                    if (info != null)
                    {
                        if (toBageType == eBageType.Store)
                        {
                        }
                        Console.WriteLine("-----Stage 5 !");
                        bag.MoveToStore(bag, slot, toSlot, inventory, info.StoreLevel * 10);                        
                        return 1;
                    }
                }

                if ((toBageType == eBageType.Store) || (bageType == eBageType.Store))
                {
                    SqlDataProvider.Data.ItemInfo itemAt = client.Player.GetItemAt(bageType, slot);
                    if ((itemAt != null) && (itemAt.Count > 1))
                    {
                        itemAt.Count -= count;
                        bag.UpdateItem(itemAt);
                        SqlDataProvider.Data.ItemInfo item = itemAt.Clone();
                        item.Count = count;
                        if (inventory.GetItemAt(toSlot) == null)
                        {
                            Console.WriteLine("-----Stage 6 !");
                            inventory.AddItemTo(item, toSlot);
                           
                        }
                        else
                        {
                            SqlDataProvider.Data.ItemInfo itemByTemplateID = bag.GetItemByTemplateID(0, inventory.GetItemAt(toSlot).TemplateID);
                            if (itemByTemplateID == null)
                            {
                                Console.WriteLine("-----Stage 7 !");
                                bag.MoveToStore(inventory, toSlot, bag.FindFirstEmptySlot(0), bag, 999);
                                
                            }
                            else
                            {
                                Console.WriteLine("-----Stage 8 !");
                                itemByTemplateID.Count++;
                                bag.UpdateItem(itemByTemplateID);
                                inventory.RemoveItemAt(toSlot);
                             
                            }
                            Console.WriteLine("-----Stage 9 !");
                            inventory.AddItemTo(item, toSlot);
                           
                        }
                    }
                    else
                    {
                        if ((((toBageType != eBageType.Store) && (toBageType != eBageType.MainBag)) && ((bag.GetItemAt(slot) != null) && (bag.GetItemAt(slot).Template.CategoryID == 7))) && (((toSlot > 0) && (toSlot < 31)) && (toSlot != 6)))
                        {
                            return 1;
                        }
                        try
                        {
                            Console.WriteLine("-----Stage 10 !");
                            bag.MoveToStore(bag, slot, toSlot, inventory, 50);
                            
                        }
                        catch (Exception)
                        {
                            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType).InfoFormat("ERROR USER CHANGEITEM placce: {0},toplace: {1},bagType: {2},ToBagType {3}", new object[] { slot, toSlot, bageType, toBageType });
                            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType).InfoFormat("item :{0}, toitem {1}", bag.GetItemAt(slot), inventory.GetItemAt(toSlot));
                        }
                    }
                    return 1;
                }
                if ((toBageType == eBageType.MainBag) && (bageType == eBageType.Bank))
                {
                    Console.WriteLine("-----Stage 11!");
                    bag.MoveToStore(bag, slot, toSlot, inventory, 50);
                    return 1;
                }
            }
            //==================================
            return 0;
        }
    }
}
