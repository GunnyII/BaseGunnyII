using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Server.GameObjects;
using Game.Base.Packets;
using Bussiness;
using Game.Server.Managers;
using Bussiness.Interface;
using SqlDataProvider.Data;

namespace Game.Server.Packets.Client
{
    [PacketHandler((int)ePackageType.BUFF_INFO, "user ac action")]
    public class BuffInfoHandler : IPacketHandler
    {

        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            

            return 0;
        }

    }
}
