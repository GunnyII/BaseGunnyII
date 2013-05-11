using Game.Base.Packets;
using Game.Server;
using Game.Server.GameUtils;
using SqlDataProvider.Data;
using System;
using System.Collections.Generic;
using log4net;
using System.Reflection;
namespace Game.Server.Packets.Client
{

    [PacketHandler((byte)ePackageType.CHANGE_PLACE_GOODS_ALL, "物品比较")]
    public class MoveGoodsAllHandler : IPacketHandler
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {

            bool isStack = packet.ReadBoolean();
            int count = packet.ReadInt();
            int bagType = packet.ReadInt();
            PlayerInventory inventory = client.Player.GetInventory((eBageType)bagType);
           
            List<ItemInfo> recoverItems = inventory.GetItems(inventory.BeginSlot, inventory.Capalility - 1);
            if (count == recoverItems.Count)
            {
                inventory.BeginChanges();
                
                bool result = false;
                try
                {
                    ItemInfo[] rawitems = inventory.GetRawSpaces();
                    inventory.ClearBag();
                    for (int i = 0; i < count; i++)
                    {
                        int oldplace = packet.ReadInt();
                        int newplace = packet.ReadInt();
                        ItemInfo it = rawitems[oldplace];
                        if (!inventory.AddItemTo(it, newplace))
                        {
                            throw new Exception(string.Format("move item error: old place:{0} new place:{1}", oldplace, newplace));
                        }
                    }
                    //=============================
                    if (isStack && bagType == 1)
                    {
                        recoverItems = inventory.GetItems(inventory.BeginSlot, inventory.Capalility - 1);

                        List<int> list2 = new List<int>();
                        for (int i = 0; i < recoverItems.Count; i++)
                        {
                            if (!list2.Contains(i))
                            {
                                for (int j = recoverItems.Count - 1; j > i; j--)
                                {
                                    if (!list2.Contains(j) && ((recoverItems[i].TemplateID == recoverItems[j].TemplateID) && recoverItems[i].CanStackedTo(recoverItems[j])))
                                    {
                                        inventory.MoveItem(recoverItems[j].Place, recoverItems[i].Place, recoverItems[j].Count);
                                        list2.Add(j);
                                    }
                                }
                            }
                        }
                    }
                    recoverItems = inventory.GetItems(inventory.BeginSlot, inventory.Capalility - 1);
                    if (inventory.FindFirstEmptySlot(inventory.BeginSlot) != -1)
                    {
                        for (int k = 1; inventory.FindFirstEmptySlot(inventory.BeginSlot) < recoverItems[recoverItems.Count - k].Place; k++)
                       {
                            inventory.MoveItem(recoverItems[recoverItems.Count - k].Place, inventory.FindFirstEmptySlot(inventory.BeginSlot), recoverItems[recoverItems.Count - k].Count);
                        }
                    }
                    //=============================
                    result = true;
                }
                catch (Exception ex)
                {
                    log.ErrorFormat("Arrage bag errror,user id:{0}   msg:{1}", client.Player.PlayerId, ex.Message);
                }
                finally
                {
                    if (!result)
                    {
                        inventory.ClearBag();
                        
                        foreach (ItemInfo info in recoverItems)
                        {
                            inventory.AddItemTo(info, info.Place);
                        }
                    }
                    inventory.CommitChanges();
                }
            }

            return 0;
        }
    }
}

