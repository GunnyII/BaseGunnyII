using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Server.GameObjects;
using Game.Base.Packets;
using Newtonsoft.Json;
using Bussiness;
using SqlDataProvider.Data;

namespace Game.Server.Packets.Client
{
    [PacketHandler((int)ePackageType.CHAT_PERSONAL, "用户与用户之间的聊天")]
    public class UserPrivateChatHandler : IPacketHandler
    {

        //修改:  Xiaov 
        //时间:  2009-11-7
        //描述:  用户与用户之间私聊<已测试>
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            int receiverID = packet.ReadInt();
            //int senderID = packet.ReadInt();
            string receiver = packet.ReadString();
            string sender = packet.ReadString();
            string msg = packet.ReadString();
            bool isAutoReply = packet.ReadBoolean();

            if (receiverID == 0)
            {
                using (PlayerBussiness db = new PlayerBussiness())
                {
                    PlayerInfo info = db.GetUserSingleByNickName(receiver);
                    if (info != null)
                        receiverID = info.ID;
                }
            }

            if (receiverID != 0)
            {
                GSPacketIn pkg = packet.Clone();
                pkg.ClearContext();
                pkg.WriteInt(receiverID);
                pkg.ClientID = (client.Player.PlayerCharacter.ID);
                //pkg.WriteInt(senderID);
                pkg.WriteString(receiver);
                pkg.WriteString(sender);
                pkg.WriteString(msg);
                pkg.WriteBoolean(false);

                GamePlayer player = Managers.WorldMgr.GetPlayerById(receiverID);
 
                if (player != null)
                {
                    if (player.IsBlackFriend(client.Player.PlayerCharacter.ID))
                        return 1;
                    player.Out.SendTCP(pkg);
                }
                else
                {

                    GameServer.Instance.LoginServer.SendPacket(pkg);
                }
                client.Out.SendTCP(pkg);
            }
            else
            {
                client.Out.SendMessage(eMessageType.ERROR, LanguageMgr.GetTranslation("UserPrivateChatHandler.NoUser"));
            }



            return 1;
        }
    }
}
