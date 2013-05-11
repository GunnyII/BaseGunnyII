using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base.Packets;
using Game.Server.GameObjects;
using Bussiness;
using SqlDataProvider.Data;

namespace Game.Server.Packets.Client
{
    [PacketHandler((byte)ePackageType.GET_PLAYER_CARD, "场景用户离开")]
    public class PlayerCardInfoHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            int user_id = packet.ReadInt();
            GamePlayer player = Managers.WorldMgr.GetPlayerById(user_id);
            PlayerInfo info;
            List<ItemInfo> items;
            if (player != null)
            {
                info = player.PlayerCharacter;
                items = player.CardBag.GetItems(0, 6);
            }
            else
            {
                using (PlayerBussiness pb = new PlayerBussiness())
                {
                    info = pb.GetUserSingleByUserID(user_id);
                    items = pb.GetUserCardEuqip(user_id);
                }
            }

            if (info != null && items != null)
            client.Out.SendPlayerCardInfo(info, items);
            return 0;
        }
    }
}
