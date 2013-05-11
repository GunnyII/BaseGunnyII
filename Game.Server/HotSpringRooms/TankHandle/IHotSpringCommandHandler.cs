namespace Game.Server.HotSpringRooms.TankHandle
{
    using Game.Base.Packets;
    using Game.Server.GameObjects;
    using Game.Server.HotSpringRooms;
    using System;

    public interface IHotSpringCommandHandler
    {
        bool HandleCommand(TankHotSpringLogicProcessor process, GamePlayer player, GSPacketIn packet);
    }
}

