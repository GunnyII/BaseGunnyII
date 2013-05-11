using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;
using log4net.Util;
using Game.Server.GameObjects;
using System.Threading;
using Bussiness;
using SqlDataProvider.Data;
using Game.Server.Statics;
using Game.Server.Packets;
using Game.Base.Packets;

namespace Game.Server.Packets.Client
{
    [PacketHandler((byte)ePackageType.DAILY_AWARD, "场景用户离开")]
    public class DailyAwardHandler : IPacketHandler
    {
        #region IPacketHandler Members
        public int HandlePacket(GameClient client, Game.Base.Packets.GSPacketIn packet)
        {
            int type_award = packet.ReadInt();
            //1:isDailyGotten
            //2:isShowEgg
            //3:Vip
            if (type_award == 3)
            {
                //client.Player.PlayerCharacter.CanTakeVipReward = false;
                client.Player.PlayerCharacter.LastVIPPackTime = DateTime.Now;
                using (PlayerBussiness db = new PlayerBussiness())
                {
                    db.UpdateLastVIPPackTime(client.Player.PlayerCharacter);
                }
            }
            if (type_award == 2)
            {

            }
            if (Managers.AwardMgr.AddDailyAward(client.Player) == true && type_award == 1)
            {
                using (PlayerBussiness db = new PlayerBussiness())
                {
                    if (db.UpdatePlayerLastAward(client.Player.PlayerCharacter.ID))
                    {
                        client.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("GameUserDailyAward.Success"));
                    }
                    else
                    {
                        client.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("GameUserDailyAward.Fail"));
                    }
                }

            }
            else
            {
                client.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("GameUserDailyAward.Fail1"));
            }
            return 2;
        }

        #endregion
    }
}
