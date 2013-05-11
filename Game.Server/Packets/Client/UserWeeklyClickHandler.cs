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
    [PacketHandler((byte)ePackageType.WEEKLY_CLICK_CNT, "场景用户离开")]
    public class UserWeeklyClickHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            GSPacketIn pkg = packet.Clone();
            //if (DateTime.Now.Date != client.Player.PlayerCharacter.LastDate.Date)
            //{
                pkg.WriteBoolean(true);
            //}
            //else
            //{
            //    pkg.WriteBoolean(false);
            //}
            client.SendTCP(pkg);
            return 0;
        }
    }
}
