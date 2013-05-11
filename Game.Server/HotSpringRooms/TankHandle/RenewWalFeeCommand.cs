namespace Game.Server.HotSpringRooms.TankHandle
{
    using Bussiness.Managers;
    using Game.Base.Packets;
    using Game.Server.GameObjects;
    using Game.Server.HotSpringRooms;
    using System;

    [HotSpringCommand((byte)HotSpringCmdType.HOTSPRING_ROOM_RENEWAL_FEE)]
    public class RenewWalFeeCommand : IHotSpringCommandHandler
    {
        public bool HandleCommand(TankHotSpringLogicProcessor process, GamePlayer player, GSPacketIn packet)
        {
            if (player.CurrentHotSpringRoom != null)
            {
                int num = packet.ReadInt();
                if (ItemMgr.FindItemTemplate(packet.ReadInt()) != null)
                {
                }
            }
            return false;
        }
    }
}

