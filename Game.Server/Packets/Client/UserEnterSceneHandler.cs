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
    [PacketHandler((int)ePackageType.SCENE_LOGIN, "Player enter scene.")]
    public class UserEnterSceneHandler:IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {            
            var typeScene = packet.ReadInt();
            switch (typeScene)
            {   
                    
                case 1:
                case 2:
                    //RoomMgr.EnterWaitingRoom(client.Player);
                    //client.Player.MainBag.UpdatePlayerProperties();
                    break;                   
                default:
                    break;
            }
            //client.Out.SendMessage(eMessageType.Normal, "Scene: " + typeScene);
            
            return 1;
        }
    }
}
