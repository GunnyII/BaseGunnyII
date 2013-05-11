using Bussiness;
using Game.Base.Packets;
using Game.Server.GameObjects;
using Game.Server.HotSpringRooms;
using Game.Server.Packets;
using Game.Server.Statics;
using SqlDataProvider.Data;
using System;

namespace Game.Server.HotSpringRooms.TankHandle
{


    [HotSpringCommand((byte)HotSpringCmdType.LARGESS)]
    public class LargessCommand : IHotSpringCommandHandler
    {
        public bool HandleCommand(TankHotSpringLogicProcessor process, GamePlayer player, GSPacketIn packet)
        {
            if (player.CurrentHotSpringRoom != null)
            {
                int num = packet.ReadInt();
                if (num <= 0)
                {
                    return false;
                }
                if (player.PlayerCharacter.Money >= num)
                {
                    player.RemoveMoney(num);
                    LogMgr.LogMoneyAdd(LogMoneyType.Marry, LogMoneyType.Marry_Gift, player.PlayerCharacter.ID, num, player.PlayerCharacter.Money, 0, 0, 0, "", "", "");
                    using (PlayerBussiness bussiness = new PlayerBussiness())
                    {
                        string translation = LanguageMgr.GetTranslation("LargessCommand.Content", new object[] { player.PlayerCharacter.NickName, num / 2 });
                        string str2 = LanguageMgr.GetTranslation("LargessCommand.Title", new object[] { player.PlayerCharacter.NickName });
                        MailInfo mail = new MailInfo {
                            Annex1 = "",
                            Content = translation,
                            Gold = 0,
                            IsExist = true,
                            Money = num / 2,
                            Receiver = player.CurrentHotSpringRoom.Info.playerName,
                            ReceiverID = player.CurrentHotSpringRoom.Info.playerID,
                            Sender = LanguageMgr.GetTranslation("LargessCommand.Sender", new object[0]),
                            SenderID = 0,
                            Title = str2,
                            Type = 14
                        };
                        bussiness.SendMail(mail);
                        player.Out.SendMailResponse(mail.ReceiverID, eMailRespose.Receiver);
                        MailInfo info2 = new MailInfo {
                            Annex1 = "",
                            Content = translation,
                            Gold = 0,
                            IsExist = true,
                            Money = num / 2,
                            Receiver = player.CurrentHotSpringRoom.Info.GroomName,
                            ReceiverID = player.CurrentHotSpringRoom.Info.GroomID,
                            Sender = LanguageMgr.GetTranslation("LargessCommand.Sender", new object[0]),
                            SenderID = 0,
                            Title = str2,
                            Type = 14
                        };
                        bussiness.SendMail(info2);
                        player.Out.SendMailResponse(info2.ReceiverID, eMailRespose.Receiver);
                    }
                    player.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("LargessCommand.Succeed", new object[0]));
                    GSPacketIn @in = player.Out.SendMessage(eMessageType.ChatNormal, LanguageMgr.GetTranslation("LargessCommand.Notice", new object[] { player.PlayerCharacter.NickName, num }));
                    player.CurrentHotSpringRoom.SendToPlayerExceptSelf(@in, player);
                    return true;
                }
                player.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("UserFirecrackersCommand.MoneyNotEnough", new object[0]));
            }
            return false;
        }
    }
}

