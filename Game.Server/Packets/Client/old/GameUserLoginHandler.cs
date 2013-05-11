using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base.Packets;
using Game.Server.GameObjects;
using Game.Server.Managers;
using Bussiness;
using Game.Server.Rooms;

namespace Game.Server.Packets.Client
{
    [PacketHandler((int)ePackageType.GAME_ROOM_LOGIN, "进入游戏")]
    public class GameUserLoginHandler : IPacketHandler
    {
       

        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            //type可能的值为 -2, -1, 1, 3, 4
            //当type = 1, 3, 4时,分别指快速加入组队战,Boss战,夺宝战
            //当type = -1时,房间ID为所点的房间ID，无密码时也传空字符串
            //当type = -2时,表示PVE游戏中的邀请
            bool isInvite=packet.ReadBoolean();
            int type = packet.ReadInt();
            int isRoundID = packet.ReadInt();
            int roomId = -1;
            string pwd = null;
          
            if (isRoundID==-1)
            {
                roomId = packet.ReadInt();
                pwd = packet.ReadString();
            }
            if (type == 1) type = 0;
            else if (type == 2) type = 4;
            
            RoomMgr.EnterRoom(client.Player, roomId, pwd, type);

            return 0;
        }
    }
}
