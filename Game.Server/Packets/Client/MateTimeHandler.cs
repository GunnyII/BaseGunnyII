using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base.Packets;
using Game.Server.Managers;
using Game.Server.GameObjects;
using Game.Server.GameUtils;
using Game.Server.Rooms;

namespace Game.Server.Packets.Client
{
    [PacketHandler((byte)ePackageType.MATE_ONLINE_TIME, "场景用户离开")]
    public class MateTimeHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            int user_id = packet.ReadInt();
            //GSPacketIn pkg = packet.Clone();
            //pkg.WriteDateTime(DateTime.Now);
            //client.SendTCP(pkg);
            return 0;
        }
    }
}
