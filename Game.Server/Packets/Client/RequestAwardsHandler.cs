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
    [PacketHandler((byte)ePackageType.CADDY_GET_AWARDS, "场景用户离开")]
    public class RequestAwardsHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            int CaddyType = packet.ReadInt();
            /*
            GSPacketIn pkg = new GSPacketIn((byte)ePackageType.CADDY_GET_AWARDS, client.Player.PlayerId);
            pkg.WriteBoolean(true);
            pkg.WriteInt(1);
            for (int i = 0; i < 1; i++)
            {
                pkg.WriteString(info3.Template.Name);
                pkg.WriteInt(info3.TemplateID);
                pkg.WriteInt(4);
                pkg.WriteBoolean(false);
            }
            client.Out.SendTCP(pkg);
             */ 
            return 0;
        }
    }
}
