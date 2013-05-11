namespace Game.Server.Packets.Client
{
    using Game.Base.Packets;
    using Game.Server;
    using System;

    [PacketHandler(0xd4, "礼堂数据")]
    public class HotSpringEnterConfirmHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            int val = packet.ReadInt();
            GSPacketIn pkg = new GSPacketIn(0xd4);
            pkg.WriteInt(val);
            client.SendTCP(pkg);
            return 0;
        }
    }
}

