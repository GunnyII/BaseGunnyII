using Game.Base.Packets;
using Game.Server.GameObjects;
using System;

namespace Game.Server.HotSpringRooms
{
   
    public abstract class AbstractHotSpringProcessor : IHotSpringProcessor
    {
        protected AbstractHotSpringProcessor()
        {
        }

        public virtual void OnGameData(HotSpringRoom game, GamePlayer player, GSPacketIn packet)
        {
        }

        public virtual void OnTick(HotSpringRoom room)
        {
        }
    }
}

