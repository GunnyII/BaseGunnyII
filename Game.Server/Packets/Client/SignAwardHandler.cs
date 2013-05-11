using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;
using log4net.Util;
using Game.Server.GameObjects;
using System.Threading;
using Bussiness;
using SqlDataProvider.Data;
using Game.Server.Statics;
using Game.Server.Packets;
using Game.Base.Packets;

namespace Game.Server.Packets.Client
{
    [PacketHandler((byte)ePackageType.GET_SIGNAWARD, "场景用户离开")]
    public class SignAwardHandler : IPacketHandler
    {        
        public int HandlePacket(GameClient client, Game.Base.Packets.GSPacketIn packet)
        {
            int type_award = packet.ReadInt();
            
            return 0;
        }

    }
}
