using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Server.GameObjects;
using Game.Base.Packets;
using SqlDataProvider.Data;
using Bussiness;

namespace Game.Server.Packets.Client
{
    [PacketHandler((byte)ePackageType.IM_CMD, "添加好友")]
    public class IMHandler : IPacketHandler
    {
        //0友好，1黑名单
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            var im_cmd = packet.ReadByte();            
            switch (im_cmd)
            {
                case (byte)IMPackageType.FRIEND_ADD:
                    HandleFRIEND_ADD(packet);
                    break;
                case (byte)IMPackageType.FRIEND_REMOVE:
                    break;
                case (byte)IMPackageType.FRIEND_UPDATE:
                    break;
                case (byte)IMPackageType.FRIEND_STATE:
                    break;
                case (byte)IMPackageType.ONS_EQUIP:
                    break;
                case (byte)IMPackageType.FRIEND_RESPONSE:
                    break;
                case (byte)IMPackageType.SAME_CITY_FRIEND:
                    break;
                case (byte)IMPackageType.ADD_CUSTOM_FRIENDS:
                    break;
                case (byte)IMPackageType.ONE_ON_ONE_TALK:
                    break;
            }
            return 0;
        }
        protected void HandleFRIEND_ADD(GSPacketIn packet)
        {

        }
    }
}
