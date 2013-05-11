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
    [PacketHandler((byte)ePackageType.USER_LUCKYNUM, "场景用户离开")]
    public class UserLuckyNumHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            //_loc_3.writeBoolean(param2);
            bool responseLuckyNum = packet.ReadBoolean();
            //_loc_3.writeInt(param1);
            int LuckyNum = packet.ReadInt();
            //GSPacketIn pkg = packet.Clone();
            //pkg.WriteInt(2);
            //pkg.WriteString("False,False,True,True");
            //client.SendTCP(pkg);
            return 1;
        }
    }
}
