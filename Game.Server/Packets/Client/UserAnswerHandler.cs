using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base.Packets;
using Game.Logic;
using SqlDataProvider.Data;
using Game.Server.Statics;

namespace Game.Server.Packets.Client
{
    [PacketHandler((int)ePackageType.USER_ANSWER, "New User Answer Question")]
    public class UserAnswerHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {

            byte unknow = packet.ReadByte();
            int id = packet.ReadInt();
            if (client.Player.PlayerCharacter.AnswerSite < id)
            {
                List<ItemInfo> infos = null;
                client.Player.PlayerCharacter.AnswerSite = id;
                if (DropInventory.AnswerDrop(id, ref infos))
                {
                    int gold = 0;
                    int money = 0;
                    int giftToken = 0;
                    int medal = 0;
                    foreach (ItemInfo info in infos)
                    {
                        ItemInfo.FindSpecialItemInfo(info, ref gold, ref money, ref giftToken, ref medal);
                        //if ((info != null) && (info.Template.BagType == eBageType.PropBag))
                        //{
                            client.Player.MainBag.AddTemplate(info, info.Count);
                        //}
                        client.Player.AddGold(gold);
                        client.Player.AddMoney(money);
                        client.Player.AddGiftToken(giftToken);
                        client.Player.AddMedal(medal);
                        LogMgr.LogMoneyAdd(LogMoneyType.Award, LogMoneyType.Award_Answer, client.Player.PlayerCharacter.ID, giftToken, client.Player.PlayerCharacter.Money, money, 0, 0,0, "", "", "");
                    }
                }
            }

            GSPacketIn pkg = packet.Clone();
            pkg.ClearContext();
            pkg.WriteInt(client.Player.PlayerCharacter.AnswerSite);           
            for (int i = 0; i < client.Player.PlayerCharacter.AnswerSite; i++)
            {
                pkg.WriteByte(unknow);                
            }
            
            client.Player.Out.SendTCP(pkg);
            return 1;
        }
    }
}
