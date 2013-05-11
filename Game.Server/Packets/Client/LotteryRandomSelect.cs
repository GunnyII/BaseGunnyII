using Bussiness;
using Bussiness.Managers;
using Game.Base.Packets;
using Game.Server;
using Game.Server.GameUtils;
using SqlDataProvider.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Server.Packets.Client
{
    [PacketHandler((byte)ePackageType.LOTTERY_RANDOM_SELECT, "打开物品")]
    public class LotteryRandomSelect : IPacketHandler
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
        public static List<int[]> listRandomitem = new List<int[]> { list1, list2, list3, list4, list5, list6, list7, list8, list9, list10, list11 };
        private static readonly List<int> RandomForGetItem = new List<int> { 
            30, 50, 70, 0x4b, 0x55, 0x56, 0x58, 0x57, 0x59, 90, 0x5c, 0x5d, 0x5e, 0x5f, 0x60, 0x61, 
            0x62, 0x63
         };

        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            int Lottery = client.Lottery;
            int[] numArray = listRandomitem[Lottery];
            PlayerInventory caddyBag = client.Player.CaddyBag;
            PlayerInventory propBag = client.Player.PropBag;
            SqlDataProvider.Data.ItemInfo itemByTemplateID = propBag.GetItemByTemplateID(0, 11444);
            try
            {
                ThreadSafeRandom random = new ThreadSafeRandom();
                int num2 = random.Next(100) * random.Next(100);
                int index = 0;
                string[] strArray = client.tempData.Split(new char[] { ',' });
                List<int> list = new List<int>();
                if (client.tempData != "start")
                {
                    foreach (string str in strArray)
                    {
                        if (!string.IsNullOrEmpty(str))
                        {
                            list.Add(int.Parse(str));
                        }
                    }
                }
                IOrderedEnumerable<int> enumerable = from s in list
                    orderby s
                    select s;
                foreach (int num4 in RandomForGetItem)
                {
                    if (num2 < (num4 * num4))
                    {
                        foreach (int num5 in enumerable)
                        {
                            if (index == num5)
                            {
                                index++;
                            }
                        }
                        break;
                    }
                    index++;
                }
                if (itemByTemplateID.Count > strArray.Length)
                {
                    itemByTemplateID.Count -= strArray.Length;
                    propBag.UpdateItem(itemByTemplateID);
                }
                else if (itemByTemplateID.Count == strArray.Length)
                {
                    propBag.RemoveItem(itemByTemplateID);
                }
                if (client.tempData == "start")
                {
                    client.tempData = index + ",";
                }
                else
                {
                    client.tempData = client.tempData + index + ",";
                }
                if (index >= numArray.Length)
                {
                    index = 0;
                }
                SqlDataProvider.Data.ItemInfo item = SqlDataProvider.Data.ItemInfo.CreateFromTemplate(ItemMgr.FindItemTemplate(numArray[index]), 1, 105);
                item.BeginDate = DateTime.Now;
                item.ValidDate = 7;
                item.RemoveDate = DateTime.Now.AddDays(7.0);
                caddyBag.AddItem(item);
                GSPacketIn pkg = new GSPacketIn(30, client.Player.PlayerId);
                pkg.WriteInt(numArray[index]);
                pkg.WriteInt(0);
                pkg.WriteInt(0);
                pkg.WriteInt(0);
                pkg.WriteInt(0);
                pkg.WriteInt(0);
                pkg.WriteInt(0);
                pkg.WriteBoolean(false);
                pkg.WriteInt(7);
                pkg.WriteByte(1);
                client.SendTCP(pkg);
            }
            catch
            {
                Console.WriteLine("Error ");
            }
            return 1;
        }
    }
}

