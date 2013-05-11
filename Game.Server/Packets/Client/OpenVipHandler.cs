using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base.Packets;
using Game.Server.Managers;
using Game.Server.GameObjects;
using Game.Server.GameUtils;
using Game.Server.Rooms;
using Bussiness;
using SqlDataProvider.Data;

namespace Game.Server.Packets.Client
{
    [PacketHandler((byte)ePackageType.VIP_RENEWAL, "场景用户离开")]
    public class OpenVipHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {

            string NickName = packet.ReadString();
            int reneval_days = packet.ReadInt();
            int result = 0;
            int money = 0;
            int ONE_MONTH_PAY = 399;
            int ONE_YEAR_PAY = 1197;
            ShopItemInfo shopItem = Bussiness.Managers.ShopMgr.GetShopItemInfoById(11992);
            if (shopItem != null)
            {
                ONE_MONTH_PAY = shopItem.AValue1;
                ONE_YEAR_PAY = shopItem.BValue1;
            }          
            
                //client.Out.SendMessage(eMessageType.Normal, "Active VIP success!");               
                switch (reneval_days / 365)
                {
                    case 1:
                    case 2:
                        money = ONE_YEAR_PAY * (reneval_days / 31);
                        break;
                    default:
                        money = ONE_MONTH_PAY * (reneval_days / 31);
                        break;
                }
                if (money <= client.Player.PlayerCharacter.Money)
                {
                    using (PlayerBussiness db = new PlayerBussiness())
                    {
                        result = db.VIPRenewal(NickName, reneval_days);                       
                    }
                    client.Player.RemoveMoney(money); 
                    client.Out.SendOpenVIP(client.Player);                   
                }
                else
                {
                    client.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("UserBuyItemHandler.Money"));
                }
           

            return result;
        }
    }
}
