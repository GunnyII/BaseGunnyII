using Game.Base.Packets;
using Game.Server;
using Bussiness;
using Game.Server.HotSpringRooms;
using SqlDataProvider.BaseClass;
using SqlDataProvider.Data;
using System;
using System.Data.SqlClient;

namespace Game.Server.Packets.Client
{
    

    [PacketHandler(0xca, "礼堂数据")]
    public class HotSpringRoomEnterDataHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            int num = packet.ReadInt();
            try
            {
                TankHotSpringLogicProcessor processor = new TankHotSpringLogicProcessor();
                HotSpringRoomInfo info = new HotSpringRoomInfo {
                    RoomID = num
                };
                client.Player.CurrentHotSpringRoom = new HotSpringRoom(info, processor);
            }
            catch
            {
                Console.WriteLine("System Error!");
            }
            HotSpringRoom room = client.Player.CurrentHotSpringRoom;
            using (PlayerBussiness db = new PlayerBussiness())
            {
                db.UpdateHotSpringRoomInfo(room.Info);
            }                
            string str = packet.ReadString();
            GSPacketIn pkg = new GSPacketIn(0xca);
            pkg.WriteInt(num);
            pkg.WriteInt(num);
            pkg.WriteString(room.Info.RoomName);
            pkg.WriteString(room.Info.Pwd);
            pkg.WriteInt(1);
            pkg.WriteInt(1);
            pkg.WriteInt(client.Player.PlayerCharacter.ID);
            pkg.WriteString(client.Player.PlayerCharacter.NickName);
            pkg.WriteDateTime(room.Info.BeginTime);
            pkg.WriteString(room.Info.RoomIntroduction);
            pkg.WriteInt(1);
            pkg.WriteInt(10);
            pkg.WriteDateTime(DateTime.Now);
            pkg.WriteInt(10);
            client.SendTCP(pkg);
            return 0;
        }
    }
}

