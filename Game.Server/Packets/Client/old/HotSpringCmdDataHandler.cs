namespace Game.Server.Packets.Client
{
    using Game.Base.Packets;
    using Game.Server;
    using Game.Server.HotSpringRooms;
    using SqlDataProvider.Data;
    using System;

    [PacketHandler(0xbf, "礼堂数据")]
    public class HotSpringCmdDataHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            packet = packet.Clone();
            packet.ClearContext();
            int num = packet.ReadInt();
            int num2 = packet.ReadInt();
            int num3 = packet.ReadInt();
            int num4 = packet.ReadInt();
            int num5 = packet.ReadInt();
            int num6 = packet.ReadInt();
            int num7 = packet.ReadInt();
            client.Player.CurrentHotSpringRoom = new HotSpringRoom(new HotSpringRoomInfo(), new TankHotSpringLogicProcessor());
            client.Player.CurrentHotSpringRoom.ProcessData(client.Player, packet);
            return 0;
        }
    }
}

