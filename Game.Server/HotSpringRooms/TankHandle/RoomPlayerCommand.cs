using Game.Base.Packets;
using Game.Server.GameObjects;
using Game.Server.HotSpringRooms;
using System;

namespace Game.Server.HotSpringRooms.TankHandle
{    
    [HotSpringCommand((byte)HotSpringCmdType.HOTSPRING_ROOM_PLAYER_CONTINUE)]
    public class RoomPlayerCommand : IHotSpringCommandHandler
    {
        public bool HandleCommand(TankHotSpringLogicProcessor process, GamePlayer player, GSPacketIn packet)
        {
            return true;
        }
    }
}

