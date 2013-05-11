using Bussiness;
using Bussiness.Managers;
using Game.Base.Packets;
using Game.Server;
using Game.Server.GameUtils;
using SqlDataProvider.Data;
using System;
using System.Collections.Generic;

namespace Game.Server.Packets.Client
{
    [PacketHandler((byte)ePackageType.EQUIP_RECYCLE_ITEM, "场景用户离开")]
    public class EquipRetrieveHandler : IPacketHandler
    {       
           public static List<int> listItemRandom = new List<int> { 
            0x4bfa0, 0x4bfa1, 0x4bfa2, 0x4bfa3, 0x4bfa4, 0x4bfa5, 0x4bfa6, 0x4bfa7, 0x4bfa8, 0x4bfa9, 0x4bfaa, 0x4bfab, 0x4bfac, 0x4bfad, 0x4bfae, 0x4bfaf, 
            0x4bfb0, 0x4bfb1, 0x4bfb2, 0x4bfb3, 0x4bfb4, 0x4bfb5, 0x4bfb6, 0x4bfb7, 0x4bfb8, 0x4bfb9, 0x4bfba, 0x4bfbb, 0x4bfbc, 0x4bfbf, 0x4bfc0, 0x4bfc1, 
            0x4bfc2, 0x4c003, 0x4c004, 0x4c005, 0x4c006, 0x4c007, 0x4c008, 0x4c009, 0x4c00a, 0x4c00b, 0x4c00c, 0x4c00d, 0x4c00e, 0x4c00f, 0x4c010, 0x4c011, 
            0x4c012, 0x4c013, 0x4c014, 0x4c015, 0x4c016, 0x4c017, 0x4c018, 0x4c019, 0x4c01a, 0x4c01b, 0x4c01c, 0x4c01d, 0x4c01e, 0x4c01f, 0x4c020, 0x4c021, 
            0x4c022, 0x4c023, 0x4c024, 0x4c025, 0x4c026
         };

        public int HandlePacket(GameClient client, GSPacketIn packet)
        {            
            PlayerInventory inventory = client.Player.GetInventory(eBageType.Store);
            PlayerInventory propBag = client.Player.PropBag;
            for (int i = 1; i < 5; i++)
            {
                if (inventory.GetItemAt(i) != null)
                {
                    inventory.RemoveItemAt(i);
                }
            }
            int ItemRandom = ThreadSafeRandom.NextStatic(listItemRandom.Count);
            SqlDataProvider.Data.ItemInfo item = SqlDataProvider.Data.ItemInfo.CreateFromTemplate(ItemMgr.FindItemTemplate(listItemRandom[ItemRandom]), 1, 105);
            inventory.AddItemTo(item, 0);
            return 1;
        }
        
    }
}
