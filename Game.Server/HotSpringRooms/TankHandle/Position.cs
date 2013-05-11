using Game.Base.Packets;
using Game.Server.GameObjects;
using Game.Server.HotSpringRooms;
using System;

namespace Game.Server.HotSpringRooms.TankHandle
{
   
    [HotSpringCommand((byte)HotSpringCmdType.TARGET_POINT)]
    public class Position : IHotSpringCommandHandler
    {
        public bool HandleCommand(TankHotSpringLogicProcessor process, GamePlayer player, GSPacketIn packet)
        {
            if ((player.CurrentHotSpringRoom != null) && (player.CurrentHotSpringRoom.RoomState == eRoomState.FREE))
            {
                string str = packet.ReadString();
                int num = packet.ReadInt();
                player.X = packet.ReadInt();
                player.Y = packet.ReadInt();
                player.CurrentHotSpringRoom.ReturnPacketForScene(player, packet);
                return true;
            }
            return false;
        }
    }
}

