using Game.Base.Packets;
using Game.Server.Packets;
using Game.Server.GameObjects;
using Game.Server.HotSpringRooms;
using Game.Server.Managers;
using System;


namespace Game.Server.HotSpringRooms.TankHandle
{

    [HotSpringCommand((byte)HotSpringCmdType.HOTSPRING_ROOM_INVITE)]
    public class InviteCommand : IHotSpringCommandHandler
    {
        public bool HandleCommand(TankHotSpringLogicProcessor process, GamePlayer player, GSPacketIn packet)
        {
            if ((player.CurrentHotSpringRoom != null) && (player.CurrentHotSpringRoom.RoomState == eRoomState.FREE))
            {
                if (player.PlayerCharacter.ID != player.CurrentHotSpringRoom.Info.PlayerID)
                {
                    return false;
                }
                GSPacketIn pkg = packet.Clone();
                pkg.ClearContext();
                GamePlayer playerById = WorldMgr.GetPlayerById(packet.ReadInt());
                if (((playerById != null) && (playerById.CurrentRoom == null)) && (playerById.CurrentHotSpringRoom == null))
                {
                    pkg.WriteByte((byte)HotSpringCmdType.HOTSPRING_ROOM_INVITE);
                    pkg.WriteInt(player.CurrentHotSpringRoom.Info.RoomID);                    
                    playerById.Out.SendTCP(pkg);
                    return true;
                }
            }
            return false;
        }
    }
}

