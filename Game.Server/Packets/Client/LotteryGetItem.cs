using Game.Base.Packets;
using Game.Server;
using Game.Server.GameUtils;
using System;

namespace Game.Server.Packets.Client
{
    [PacketHandler((byte)ePackageType.LOTTERY_GET_ITEM, "打开物品")]
    public class LotteryGetItem : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            int num = packet.ReadByte();
            int num2 = packet.ReadInt();
            PlayerInventory caddyBag = client.Player.CaddyBag;
            PlayerInventory propBag = client.Player.PropBag;
            for (int i = 0; i < caddyBag.Capalility; i++)
            {
                if (caddyBag.GetItemAt(i) != null)
                {
                    caddyBag.MoveToStore(caddyBag, i, propBag.FindFirstEmptySlot(0), propBag, 999);
                }
            }
            return 1;
        }
    }
}

