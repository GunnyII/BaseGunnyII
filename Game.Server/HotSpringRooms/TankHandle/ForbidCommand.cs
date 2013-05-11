using Game.Base.Packets;
using Game.Server.GameObjects;
using Game.Server.HotSpringRooms;
using System;

namespace Game.Server.HotSpringRooms.TankHandle
{
    [HotSpringCommand((byte)HotSpringCmdType.FORBID)]
    public class ForbidCommand : IHotSpringCommandHandler
    {
        public bool HandleCommand(TankHotSpringLogicProcessor process, GamePlayer player, GSPacketIn packet)
        {
            if ((player.CurrentHotSpringRoom != null) && ((player.PlayerCharacter.ID == player.CurrentHotSpringRoom.Info.GroomID) || (player.PlayerCharacter.ID == player.CurrentHotSpringRoom.Info.playerID)))
            {
                int userID = packet.ReadInt();
                if ((userID != player.CurrentHotSpringRoom.Info.playerID) && (userID != player.CurrentHotSpringRoom.Info.GroomID))
                {
                    player.CurrentHotSpringRoom.KickPlayerByUserID(player, userID);
                    player.CurrentHotSpringRoom.SetUserForbid(userID);
                }
                return true;
            }
            return false;
        }
    }
}

