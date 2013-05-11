using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Server.GameObjects;
using Game.Base.Packets;
using Bussiness.Managers;
using SqlDataProvider.Data;
using Game.Server.GameUtils;
using Bussiness;
using Game.Server.Statics;

namespace Game.Server.Packets.Client
{
    [PacketHandler((byte)ePackageType.LOTTERY_OPEN_BOX, "打开物品")]
    public class LotteryOpenBoxHandler : IPacketHandler
    {
        public static int[] list1 = new int[] { 
            0x1f4e, 0x1f46, 0x23f3, 0x3abd, 0x1b63, 0x206e, 0x1fa6, 0x52d, 0x24be, 0x1b60, 0x1b5e, 0x1b7f, 0x1b80, 0x2138, 0x20d4, 0x1b70, 
            0x1b72, 0x1b73
         };
        public static int[] list10 = new int[] { 
            0x1f44, 0x1f45, 0x1b5d, 0x1441, 0x1b64, 0x23a2, 0x2c00, 0x2ede, 0x4269, 0x111ad, 0x111ff, 0x36bc, 0x4c776, 0x4c069, 0x4c070, 0x1127f, 
            0x11213, 0x1130b
         };
        public static int[] list11 = new int[] { 
            0x2b0e, 0x2aff, 0x23a3, 0x23a5, 0x2e82, 0x36b9, 0x36ba, 0x4269, 0x111ad, 0x111ff, 0x426a, 0x11211, 0x11231, 0x11259, 0x11275, 0x1127f, 
            0x11289, 0x1130b
         };
        public static int[] list2 = new int[] { 
            0x1f42, 0x1f4e, 0x2e81, 0x2392, 0x245a, 0x111df, 0x1130b, 0x2457, 0x251f, 0x112a7, 0x36ba, 0x1b70, 0x11301, 0x1b7f, 0x1b7f, 0x1b7f, 
            0x426a, 0x36b6
         };
        public static int[] list3 = new int[] { 
            0x2b03, 0x2aff, 0x2afb, 0x1f44, 0x1f45, 0x1f46, 0x207a, 0x2070, 0x206f, 0x2072, 0x1b5d, 0x1b6a, 0x1b69, 0x1b68, 0x1b67, 0x1b70, 
            0x1b77, 0x1b81
         };
        public static int[] list4 = new int[] { 
            0x2392, 0x23f5, 0x2e80, 0x2e7f, 0x111c1, 0x2e81, 0x36ba, 0x4269, 0x111ad, 0x111ff, 0x36b6, 0x11211, 0x4c76f, 0x4c777, 0x4c06a, 0x33f4, 
            0x11207, 0x11261
         };
        public static int[] list5 = new int[] { 
            0x2b0d, 0x1483, 0x1b5f, 0x1fa6, 0x2bff, 0x2ede, 0x3392, 0x4269, 0x111ad, 0x111ff, 0x36b6, 0x36b7, 0x4c771, 0x4c778, 0x4c06b, 0x33f5, 
            0x11207, 0x11275
         };
        public static int[] list6 = new int[] { 
            0x2af9, 0x1488, 0x1b60, 0x238e, 0x2bfe, 0x2edf, 0x3393, 0x4269, 0x111ad, 0x111ff, 0x36b6, 0x36b8, 0x4c772, 0x4c779, 0x4c06c, 0x33f6, 
            0x11208, 0x1127f
         };
        public static int[] list7 = new int[] { 
            0x2afd, 0x148b, 0x1b61, 0x238f, 0x2bfd, 0x2edd, 0x3394, 0x4269, 0x111ad, 0x111ff, 0x36b6, 0x36b9, 0x4c773, 0x4c77a, 0x4c06d, 0x33f7, 
            0x11209, 0x11289
         };
        public static int[] list8 = new int[] { 
            0x1f44, 0x2b02, 0x1491, 0x1b62, 0x2391, 0x2bfa, 0x2ede, 0x3395, 0x4269, 0x111ad, 0x111ff, 0x36ba, 0x4c774, 0x4c77b, 0x4c06e, 0x3361, 
            0x11211, 0x112b1
         };
        public static int[] list9 = new int[] { 
            0x1f45, 0x1499, 0x1b63, 0x2392, 0x2c00, 0x2ede, 0x3396, 0x4269, 0x111ad, 0x111ff, 0x36bd, 0x36bb, 0x4c775, 0x4c77c, 0x4c06f, 0x3362, 
            0x11212, 0x112ed
         };
        //public static int[] business = new Bussiness.ProduceBussiness().GetSingleItemsBox(112047);
        public static List<int> listItemRandomForBox = new List<int> { 
            0x48e, 0x48d, 0x4f1, 0x4f2, 0x2b0d, 0x2b0e, 0x2b0f, 0x2b10, 0x853, 0x8b7, 0xc4d, 0x1b5d, 0x1b5e, 0x1b5f, 0x1b60, 0x1b61, 
            0x1b62, 0x1b63, 0x1b6a, 0x1b69, 0x1b68, 0x1b67, 0x2e7d, 0x2e7e, 0x2e7f, 0x2406, 0x2407, 0x2408, 0x2409, 0x2456, 0x2457, 0x2459, 
            0x245a, 0x246a, 0x141e, 0x1482, 0x1424, 0x1487, 0x4269, 0x426a, 0x426b, 0x426c, 0x1b70, 0x1b72, 0x1b73
         };
        
        public static List<int[]> listRandomitem = new List<int[]> { list1, list2, list3, list4, list5, list6, list7, list8, list9, list10, list11 };
        public static List<int> listTemplate = new List<int> { 0x4bed8, 0x4c2c0, 0x4bf3c, 0x4bf9f, 0x4bfa0, 0x4c003, 0x4c004, 0x4c067, 0x4c068, 0x4c0cb, 0x4c2bf, 0x4c89b, 0x4c89c, 0x4c8ff, 0x4ca8f, 0x4c6a8 };
        public static List<int> RandomForGetItem = new List<int> { 
            30, 0x20, 0x22, 0x23, 40, 0x2d, 0x31, 50, 0x3f, 0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 70, 
            0x47, 0x48, 0x49, 0x4a, 0x4b, 0x4c, 0x4d, 0x4e, 0x4f, 80, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 
            0x3a, 0x57, 0x59, 90, 0x5b, 0x5c, 0x5d, 0x5e, 0x5f, 0x60, 0x61, 0x62, 0x63
         };
       
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            GSPacketIn pkg;
            int num2;
            int num3;
            var business = new Bussiness.ProduceBussiness();
               
            if (client.Lottery != -1)
            {
                client.Out.SendMessage(eMessageType.Normal, "Rương đang hoạt động!");
                return 1;
            }
            int num = new Random().Next(0, listRandomitem.Count - 1);
            client.Lottery = num;
            int num4 = packet.ReadByte();
            int slot = packet.ReadInt();
            int templateId = packet.ReadInt();
            int money = 0;
            int gold = 0;
            int giftToken = 0;
            int medal = 0;
            PlayerInventory inventory = client.Player.GetInventory((eBageType)num4);
            PlayerInventory propBag = client.Player.PropBag;
            ItemInfo item = propBag.GetItemByTemplateID(0, 11456); //búa đập lu
            ItemInfo itemAt = inventory.GetItemAt(slot);
            List<ItemInfo> infos = new List<ItemInfo>();
            
            if ((itemAt != null) && (itemAt.TemplateID == 112019))
            {
                client.tempData = "start";
                if (itemAt.Count > 1)
                {
                    itemAt.Count--;
                    propBag.UpdateItem(itemAt);
                }
                else
                {
                    propBag.RemoveItem(itemAt);
                }
                pkg = new GSPacketIn((byte)ePackageType.LOTTERY_ALTERNATE_LIST, client.Player.PlayerId);
                foreach (int num10 in listRandomitem[num])
                {
                    pkg.WriteInt(num10);
                    pkg.WriteBoolean(false);
                    pkg.WriteByte(1);
                    pkg.WriteByte(1);
                }
                client.Out.SendTCP(pkg);
                return 1;
            }
            
            if ((((templateId != 11252) && (templateId != 11257)) && ((templateId != 11258) && (templateId != 11259))) && (templateId != 11260))
            {
                if (templateId != 112047 && templateId != 112100 && templateId != 112101)
                {
                    int num12 = 0;
                    List<int> list9 = new List<int>();
                    List<ItemInfo> list10 = new List<ItemInfo>();
                    foreach (int num10 in listTemplate)
                    {
                        int itemCount = propBag.GetItemCount(0, num10);
                        if (itemCount != 0)
                        {
                            num12 += itemCount;
                            list9.Add(num10);
                            ItemTemplateInfo goods = ItemMgr.FindItemTemplate(num10);
                            list10.Add(ItemInfo.CreateFromTemplate(goods, 1, 105));
                        }
                    }
                    if (list10.Count > 0)
                    {
                        int num14 = new Random().Next(0, list10.Count);
                        ItemInfo info5 = list10[num14];
                        int[] bag = new int[3];
                        this.OpenUpItem(info5.Template.Data, bag, infos, ref gold, ref money, ref giftToken, ref medal);
                        if (infos.Count > 0)
                        {
                            pkg = new GSPacketIn((byte)ePackageType.CADDY_GET_AWARDS, client.Player.PlayerId);
                            pkg.WriteBoolean(true);
                            num3 = 1;
                            pkg.WriteInt(num3);
                            for (num2 = 0; num2 < num3; num2++)
                            {
                                pkg.WriteString(infos[0].Template.Name);
                                pkg.WriteInt(infos[0].TemplateID);
                                pkg.WriteInt(4);
                                pkg.WriteBoolean(false);
                            }
                            client.Out.SendTCP(pkg);
                            inventory.AddItem(infos[0]);
                            ItemInfo info6 = propBag.GetItemByTemplateID(0, info5.TemplateID);
                            if (info6.Count > 0)
                            {
                                info6.Count--;
                                propBag.UpdateItem(info6);
                            }
                            else
                            {
                                propBag.RemoveItem(info6);
                            }
                        }
                    }
                    client.Lottery = -1;
                    return 1;
                }

                else
                {
                    ItemInfo itemByTemplateID = propBag.GetItemByTemplateID(0, templateId);
                    
                    if ((itemByTemplateID != null) && (item != null))
                    {
                        Console.WriteLine("User open ItemBoxID: " + itemByTemplateID.TemplateID);
                        if (itemByTemplateID.Count > 1)
                        {
                            itemByTemplateID.Count--;
                            propBag.UpdateItem(itemByTemplateID);
                        }
                        else
                        {
                            propBag.RemoveItem(itemByTemplateID);
                        }
                        if (item.Count > 4)
                        {
                            item.Count -= 4;
                            propBag.UpdateItem(item);
                        }
                        else
                        {
                            propBag.RemoveItem(item);
                        }
                        ThreadSafeRandom random = new ThreadSafeRandom();
                        int num15 = random.Next(100) * random.Next(100);
                        num2 = 0;
                        foreach (int num16 in RandomForGetItem)
                        {
                            if (num15 < (num16 * num16))
                            {
                                break;
                            }
                            num2++;
                        }
                        pkg = new GSPacketIn((byte)ePackageType.CADDY_GET_AWARDS, client.Player.PlayerId);
                        pkg.WriteBoolean(true);
                        ItemInfo info9 = ItemInfo.CreateFromTemplate(ItemMgr.FindItemTemplate(listItemRandomForBox[num2]), 1, 105);
                        //ItemInfo info9 = ItemInfo.CreateFromTemplate(ItemMgr.FindItemTemplate(business.GetSingleItemsBox(itemByTemplateID.TemplateID)[num2].TemplateId), 1, 105);
                        num3 = 1;
                        pkg.WriteInt(num3);
                        pkg.WriteString(client.Player.PlayerCharacter.NickName);
                        //pkg.WriteInt(business.GetSingleItemsBox(itemByTemplateID.TemplateID)[num2].TemplateId);
                        pkg.WriteInt(listItemRandomForBox[num2]);
                        pkg.WriteInt(4);
                        pkg.WriteBoolean(false);
                        client.Out.SendTCP(pkg);
                        info9.BeginDate = DateTime.Now;
                        info9.ValidDate = 7;
                        info9.RemoveDate = DateTime.Now.AddDays(7.0);
                        inventory.AddItem(info9);
                    }

                }
            }
            else
            {
                List<int> list2 = new List<int>();
                switch (templateId)
                {
                    case 11252:
                        {
                            List<int> list3 = new List<int> { 
                            0x1fa8, 0x2bcc, 0x2edd, 0x2e7f, 0x1b6e, 0x2b1f, 0x2b21, 0x2b23, 0x2b25, 0x2b27, 0x36c0, 0x36c2, 0x36c4, 0x36c6, 0x36c8, 0x2e7d, 
                            0x2e7e, 0x2e7f, 0x2edd, 0x2e81
                         };
                            list2 = list3;
                            break;
                        }
                    case 11257:
                        {
                            List<int> list4 = new List<int> { 
                            0x1fa8, 0x1f44, 0x1f46, 0x1f4e, 0x1fa7, 0x1b6e, 0x2b21, 0x2b23, 0x2b25, 0x2b27, 0x36c0, 0x36c2, 0x36c4, 0x36c6, 0x36c8, 0x2e7d, 
                            0x2e7e, 0x2e7f, 0x2e80, 0x2e81
                         };
                            list2 = list4;
                            break;
                        }
                    case 11258:
                        {
                            List<int> list5 = new List<int> { 
                            0x2b10, 0x2b0f, 0x2b04, 0x2b00, 0x23f2, 0x2072, 0x2b21, 0x2b23, 0x23f2, 0x2b27, 0x36c0, 0x36c2, 0x36c4, 0x36c6, 0x36c8, 0x2e7d, 
                            0x23f3, 0x2e7f, 0x2e80, 0x2e81
                         };
                            list2 = list5;
                            break;
                        }
                    case 11259:
                        {
                            List<int> list6 = new List<int> { 
                            0x2b10, 0x2b0f, 0x1b6e, 0x1b5e, 0x23f2, 0x2072, 0x2b21, 0x2b23, 0x23f2, 0x2b27, 0x1b61, 0x36c2, 0x36c4, 0x1b67, 0x36c8, 0x2e7d, 
                            0x23f3, 0x2e7f, 0x2e80, 0x2e81
                         };
                            list2 = list6;
                            break;
                        }
                    case 11260:
                        {
                            List<int> list7 = new List<int> { 
                            0x1fa8, 0x2bcc, 0x2e80, 0x2e7f, 0x2b1d, 0x2b1f, 0x2b21, 0x2b23, 0x2b25, 0x2b27, 0x36c0, 0x36c2, 0x36c4, 0x36c6, 0x36c8, 0x2e7d, 
                            0x2e7e, 0x2e7f, 0x2e80, 0x2e81
                         };
                            list2 = list7;
                            break;
                        }
                    default:
                        {
                            List<int> list8 = new List<int> { 
                            0x1b72, 0x20d2, 0x1b6e, 0x11261, 0x23f2, 0x1124d, 0x2b21, 0x2b23, 0x23f2, 0x2b27, 0x2b00, 0x36c2, 0x36c4, 0x1b67, 0x36c8, 0x2e7d, 
                            0x23f3, 0x2e82, 0x334a, 0x334e
                         };
                            list2 = list8;
                            break;
                        }
                }
                int num11 = new Random().Next(0, list2.Count - 1);
                ItemInfo info3 = ItemInfo.CreateFromTemplate(ItemMgr.FindItemTemplate(list2[num11]), 1, 105);
                pkg = new GSPacketIn((byte)ePackageType.CADDY_GET_AWARDS, client.Player.PlayerId);
                pkg.WriteBoolean(true);
                num3 = 1;
                pkg.WriteInt(num3);
                for (num2 = 0; num2 < num3; num2++)
                {
                    pkg.WriteString(info3.Template.Name);
                    pkg.WriteInt(info3.TemplateID);
                    pkg.WriteInt(4);
                    pkg.WriteBoolean(false);
                }
                client.Out.SendTCP(pkg);
                inventory.AddItem(info3);
                ItemInfo info4 = client.Player.PropBag.GetItemByTemplateID(0, templateId);
                if (info4.Count > 1)
                {
                    info4.Count--;
                    client.Player.PropBag.UpdateItem(info4);
                }
                else
                {
                    client.Player.PropBag.RemoveItem(info4);
                }
                client.Lottery = -1;
                return 1;
            }
            client.Lottery = -1;
            return 1;
        }

        public void OpenUpItem(string data, int[] bag, List<ItemInfo> infos, ref int gold, ref int money, ref int giftToken, ref int medal)
        {
            if (!string.IsNullOrEmpty(data))
            {
                ItemBoxMgr.CreateItemBox(Convert.ToInt32(data), infos, ref gold, ref money, ref giftToken, ref medal);
            }
        }
    }
}
