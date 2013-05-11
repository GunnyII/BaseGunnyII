namespace Game.Server.Packets.Client
{
    using Game.Base.Packets;
    using Game.Server;
    using System;

    [PacketHandler(0xaf, "礼堂数据")]
    public class HotSpringRoomCreateHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            if (client.Player.CurrentHotSpringRoom != null)
            {
                client.Player.CurrentHotSpringRoom.ProcessData(client.Player, packet);
            }
            return 0;
        }
    }
}

