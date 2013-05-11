using Game.Base.Packets;
using Game.Server.GameObjects;
using System;

namespace Game.Server.HotSpringRooms
{
    
    public interface IHotSpringProcessor
    {
        void OnGameData(HotSpringRoom game, GamePlayer player, GSPacketIn packet);
        void OnTick(HotSpringRoom room);
    }
}

