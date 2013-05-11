using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base.Packets;

namespace Game.Server.Packets.Client
{
    [PacketHandler((byte)ePackageType.GAME_CMD, "游戏数据")]
    public class GameDataHandler:IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
                        
            if (client.Player.CurrentRoom != null)
            {
                packet.ClientID = client.Player.PlayerId;
                packet.Parameter1 = client.Player.GamePlayerId;
                client.Player.CurrentRoom.ProcessData(packet);
            }
            return 0;
        }

    }
}
