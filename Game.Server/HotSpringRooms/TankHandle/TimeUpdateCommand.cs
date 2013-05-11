using Bussiness;
using Bussiness.Managers;
using Game.Base.Packets;
using Game.Server.GameObjects;
using Game.Server.HotSpringRooms;
using Game.Server.Packets;
using Game.Server.Statics;
using SqlDataProvider.Data;
using System;
using System.Linq;

namespace Game.Server.HotSpringRooms.TankHandle
{
    
    [HotSpringCommand((byte)HotSpringCmdType.HOTSPRING_ROOM_TIME_UPDATE)]
    public class TimeUpdateCommand : IHotSpringCommandHandler
    {
        public bool HandleCommand(TankHotSpringLogicProcessor process, GamePlayer player, GSPacketIn packet)
        {
            if (player.CurrentHotSpringRoom != null)
            {
                
            }
            return false;
        }
    }
}

