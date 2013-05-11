using Bussiness;
using Game.Base.Packets;
using Game.Server.GameObjects;
using Game.Server.HotSpringRooms;
using Game.Server.Managers;
using Game.Server.Packets;
using Game.Server.Statics;
using log4net;
using System;
using System.Reflection;

namespace Game.Server.HotSpringRooms.TankHandle
{

    [HotSpringCommand((byte)HotSpringCmdType.HOTSPRING_ROOM_PLAYER_CONTINUE)]
    public class ContinuationCommand : IHotSpringCommandHandler
    {
        protected static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public bool HandleCommand(TankHotSpringLogicProcessor process, GamePlayer player, GSPacketIn packet)
        {
            if (player.CurrentHotSpringRoom == null)
            {
                return false;
            }
            if ((player.PlayerCharacter.ID != player.CurrentHotSpringRoom.Info.GroomID) && (player.PlayerCharacter.ID != player.CurrentHotSpringRoom.Info.playerID))
            {
                return false;
            }
            int time = packet.ReadInt();
            string[] strArray = GameProperties.PRICE_MARRY_ROOM.Split(new char[] { ',' });
            if (strArray.Length < 3)
            {
                if (log.IsErrorEnabled)
                {
                    log.Error("HotSpringRoomCreateMoney node in configuration file is wrong");
                }
                return false;
            }
            int num2 = 0;
            switch (time)
            {
                case 2:
                    num2 = int.Parse(strArray[0]);
                    break;

                case 3:
                    num2 = int.Parse(strArray[1]);
                    break;

                case 4:
                    num2 = int.Parse(strArray[2]);
                    break;

                default:
                    num2 = int.Parse(strArray[2]);
                    time = 4;
                    break;
            }
            if (player.PlayerCharacter.Money < num2)
            {
                player.Out.SendMessage(eMessageType.ChatNormal, LanguageMgr.GetTranslation("MarryApplyHandler.Msg1", new object[0]));
                return false;
            }
            player.RemoveMoney(num2);
            //LogMgr.LogMoneyAdd(LogMoneyType.Marry, LogMoneyType.Marry_RoomAdd, player.PlayerCharacter.ID, num2, player.PlayerCharacter.Money, 0, 0, 0, "", "", "");
            CountBussiness.InsertSystemPayCount(player.PlayerCharacter.ID, num2, 0, 0, 6);
            player.CurrentHotSpringRoom.RoomContinuation(time);
            GSPacketIn pkg = player.Out.SendContinuation(player, player.CurrentHotSpringRoom.Info);
            int playerId = 0;
            if (player.PlayerCharacter.ID == player.CurrentHotSpringRoom.Info.GroomID)
            {
                playerId = player.CurrentHotSpringRoom.Info.playerID;
            }
            else
            {
                playerId = player.CurrentHotSpringRoom.Info.GroomID;
            }
            GamePlayer playerById = WorldMgr.GetPlayerById(playerId);
            if (playerById != null)
            {
                playerById.Out.SendTCP(pkg);
            }
            player.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("ContinuationCommand.Successed"));
            return true;
        }
    }
}

