using Game.Base.Packets;
using Game.Server;
using SqlDataProvider.BaseClass;
using System;
using System.Data.SqlClient;
using Bussiness;
using Game.Server.HotSpringRooms;

namespace Game.Server.Packets.Client
{
   
    [PacketHandler(0xa9, "礼堂数据")]
    public class HotSpringRoomPlayerRemoveHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            if (client.Player.CurrentHotSpringRoom != null)
            {
                int iD = client.Player.CurrentHotSpringRoom.Info.RoomID;
                HotSpringRoom room = client.Player.CurrentHotSpringRoom;
                using (PlayerBussiness db = new PlayerBussiness())
                {
                    db.UpdateHotSpringRoomInfo(room.Info);
                }
                
                GSPacketIn pkg = new GSPacketIn(0xa9);
                pkg.WriteString("Bạn đã thoát khỏi phòng");
                client.SendTCP(pkg);
            }
            return 0;
        }
    }
}

