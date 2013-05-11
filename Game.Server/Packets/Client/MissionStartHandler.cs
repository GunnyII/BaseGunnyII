using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base.Packets;
using Game.Logic.Phy.Object;
using Bussiness;
using Game.Server.Rooms;
using Game.Logic;
using Game.Server.Statics;

namespace Game.Server.Packets.Client
{
    [PacketHandler((byte)ePackageType.GAME_MISSION_START, "游戏开始")]
    public class GameUserStartHandler:IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet )
        
        {           
            bool isReady = packet.ReadBoolean();
            if (isReady == true)
            {               
                    RoomMgr.StartGameMission(client.Player.CurrentRoom);
                    //client.Out.SendGameMissionStart(client.Player);

            }
           
            return 0;
        }
    }
}
