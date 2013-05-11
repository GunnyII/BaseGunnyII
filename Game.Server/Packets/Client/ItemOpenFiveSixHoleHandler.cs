using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Server.GameObjects;
using Game.Base.Packets;
using Bussiness.Managers;
using SqlDataProvider.Data;
using Game.Server.GameUtils;
using Bussiness;
using Game.Server.Statics;

namespace Game.Server.Packets.Client
{
    [PacketHandler((byte)ePackageType.OPEN_FIVE_SIX_HOLE, "打开物品")]
    public class ItemOpenFiveSixHoleHandler : IPacketHandler
    {        
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            //itemPlace, _openHoleNumber, _drill.TemplateID
            int itemPlace = packet.ReadInt();
            int openHoleNumber = packet.ReadInt();
            int drillTemplateID = packet.ReadInt();
            string msg = "";
            bool result = false;
            int type = 1;
            Random randomExp = new Random();
            ItemInfo item = client.Player.GetItemAt(eBageType.Store, itemPlace);            
            int exp = randomExp.Next(2, 6);
            client.Player.RemoveTemplate(drillTemplateID, 1);
            //client.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("OpenHoleHandler.GetExp", exp));
            if (openHoleNumber == 5)
                {
                    item.Hole5Exp += exp;
                    if ((item.Hole5Exp >= 400 && item.Hole5Level == 0) ||
                        (item.Hole5Exp >= 600 && item.Hole5Level == 1) ||
                        (item.Hole5Exp >= 700 && item.Hole5Level == 2) ||
                        (item.Hole5Exp >= 800 && item.Hole5Level == 3))
                    {
                        result = true;
                        type = 0;
                        //item.StrengthenTimes++;
                        item.Hole5Level++;
                        item.Hole5Exp = 0;
                        if (item.Hole5Level >= 2)
                        {
                            msg = LanguageMgr.GetTranslation("OpenHoleHandler.congratulation", client.Player.PlayerCharacter.NickName, 5, item.Template.Name, item.Hole5Level);

                        }
                    }
                    
                }
                else
                {
                    item.Hole6Exp += exp;
                    if ((item.Hole6Exp >= 400 && item.Hole6Level == 0) ||
                        (item.Hole6Exp >= 600 && item.Hole6Level == 1) ||
                        (item.Hole6Exp >= 700 && item.Hole6Level == 2) ||
                        (item.Hole6Exp >= 800 && item.Hole6Level == 3))
                    {
                        result = true;
                        type = 0;
                        //item.StrengthenTimes++;                       
                        item.Hole6Level++;                        
                        item.Hole6Exp = 0;
                        if (item.Hole6Level >= 2)
                        {
                            msg = LanguageMgr.GetTranslation("OpenHoleHandler.congratulation", client.Player.PlayerCharacter.NickName, 6, item.Template.Name, item.Hole6Level);

                        }
                    }
                    
                }
            
            client.Player.StoreBag2.UpdateItem(item);
            client.Player.SaveIntoDatabase();
            if (result && type == 0)
            {
                client.Out.SendOpenHoleComplete(client.Player, type, result);
            }
            if (msg != "")
            {
                GSPacketIn pkg = new GSPacketIn((byte)ePackageType.SYS_NOTICE);
                pkg.WriteInt(1);
                pkg.WriteString(msg);
                GameServer.Instance.LoginServer.SendPacket(pkg);
                GamePlayer[] players = Game.Server.Managers.WorldMgr.GetAllPlayers();
                foreach (GamePlayer p in players)
                {
                    p.Out.SendTCP(pkg);
                }
            }           
            return 0;
        }

        
    }
}
