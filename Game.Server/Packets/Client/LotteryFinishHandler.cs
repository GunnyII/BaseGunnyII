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
    [PacketHandler((byte)ePackageType.LOTTERY_FINISH, "打开物品")]
    public class LotteryFinishBoxHandler : IPacketHandler
    {


        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            int numReadByte = packet.ReadByte();
            int numReadInt = packet.ReadInt();
            PlayerInventory caddyBag = client.Player.CaddyBag;
            PlayerInventory propBag = client.Player.PropBag;
            PlayerInventory storeBag = client.Player.StoreBag;
            PlayerInventory mainBag = client.Player.MainBag;
            for (int i = 0; i < caddyBag.Capalility; i++)
            {
                SqlDataProvider.Data.ItemInfo itemAt = caddyBag.GetItemAt(i);
                if (itemAt != null)
                {
                    if (((itemAt.Template.CategoryID == 10) || (itemAt.Template.CategoryID == 11)) || (itemAt.Template.CategoryID == 12))
                    {
                        caddyBag.MoveToStore(caddyBag, i, propBag.FindFirstEmptySlot(1), propBag, 999);
                    }
                    else if ((itemAt.Template.CategoryID == 7) && (mainBag.GetItemAt(6) == null))
                    {
                        caddyBag.MoveToStore(caddyBag, i, 6, mainBag, 999);
                    }
                    else
                    {
                        caddyBag.MoveToStore(caddyBag, i, mainBag.FindFirstEmptySlot(31), mainBag, 999);
                    }
                }
            }
            client.Lottery = -1;
            return 1;
        }
    }
}
