using Game.Base.Packets;
using Game.Server;
using Game.Server.HotSpringRooms;
using Game.Server.Managers;
using System;

namespace Game.Server.Packets.Client
{
    [PacketHandler(0xbb, "礼堂数据")]
    public class HotSpringEnterHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            HotSpringRoom[] allHotRoom = HotSpringRoomMgr.GetAllHotSpringRoom();
            //foreach (HotSpringRoom room in allHotRoom)
            //{
            client.Player.Out.SendHotSpringRoomInfo(client.Player, allHotRoom);
            //}
            return 0;
        }
    }
}

