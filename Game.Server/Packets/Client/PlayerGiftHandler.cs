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
    [PacketHandler((byte)ePackageType.USER_GET_GIFTS, "场景用户离开")]
    public class PlayerGiftHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            var user_id = packet.ReadInt();
            /*
            GSPacketIn pkg = packet.Clone();
            packet.ClearContext();
            pkg.WriteInt(client.Player.PlayerCharacter.ID); //_self.ID
            //_loc_5.charmGP = _loc_3.readInt();
            //_loc_2 = _loc_3.readInt();
            int GiftID = 325100;
            pkg.WriteInt(10000);
            pkg.WriteInt(5);
            for (int i = 0; i < 5; i++)
            {

                //_loc_11.TemplateID = _loc_3.readInt();
                //_loc_11.amount = _loc_3.readInt();
                pkg.WriteInt(GiftID + i);
                pkg.WriteInt(1);
                //GiftID++;
            }
            client.SendTCP(pkg);
             */ 
            return 0;
        }
    }
}
