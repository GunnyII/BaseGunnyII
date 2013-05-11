using Game.Base.Packets;
using Game.Server.GameObjects;
using Game.Server.HotSpringRooms;
using System;

namespace Game.Server.HotSpringRooms.TankHandle
{

    [HotSpringCommand((byte)HotSpringCmdType.HOTSPRING_ROOM_ADMIN_REMOVE_PLAYER)]
    public class KickCommand : IHotSpringCommandHandler
    {
        public bool HandleCommand(TankHotSpringLogicProcessor process, GamePlayer player, GSPacketIn packet)
        {
            if (((player.CurrentHotSpringRoom != null) && 
                (player.CurrentHotSpringRoom.RoomState == eRoomState.FREE)) && 
                //((player.PlayerCharacter.ID == player.CurrentHotSpringRoom.Info.GroomID) || 
                (player.PlayerCharacter.ID == player.CurrentHotSpringRoom.Info.PlayerID))
            {
                int userID = packet.ReadInt();
                player.CurrentHotSpringRoom.KickPlayerByUserID(player, userID);
                return true;
            }
            return false;
        }
    }
}

