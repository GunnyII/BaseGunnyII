using Bussiness;
using Game.Server.Packets;
using Game.Base.Packets;
using Game.Server.GameObjects;
using Game.Server.HotSpringRooms.TankHandle;
using Game.Server.Managers;
using log4net;
using System;
using System.Reflection;

namespace Game.Server.HotSpringRooms
{
    [HotSpringProcessor(9, "礼堂逻辑")]
    public class TankHotSpringLogicProcessor : AbstractHotSpringProcessor
    {
        public TankHotSpringLogicProcessor()
        {
            _commandMgr = new HotSpringCommandMgr();
        }
        private HotSpringCommandMgr _commandMgr = new HotSpringCommandMgr();
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private ThreadSafeRandom random = new ThreadSafeRandom();
        public readonly int TIMEOUT = 1 * 60 * 1000;

        public override void OnGameData(HotSpringRoom room, GamePlayer player, GSPacketIn packet)
        {
            HotSpringCmdType type = (HotSpringCmdType) packet.ReadByte();
            try
            {
                IHotSpringCommandHandler handler = _commandMgr.LoadCommandHandler((int) type);
                if (handler != null)
                {
                    handler.HandleCommand(this, player, packet);
                }
                else
                {
                    log.Error(string.Format("IP: {0}", player.Client.TcpEndpoint));
                }
            }
            catch (Exception exception)
            {
                log.Error(string.Format("IP:{1}, OnGameData is Error: {0}", exception.ToString(), player.Client.TcpEndpoint));
            }
        }

        public override void OnTick(HotSpringRoom room)
        {
            try
            {
                if (room != null)
                {
                    room.KickAllPlayer();
                    GSPacketIn pkg = new GSPacketIn((short)ePackageType.HOTSPRING_ROOM_REMOVE);
                    pkg.WriteInt(room.Info.RoomID);
                    WorldMgr.HotSpring.SendToALL(pkg);
                    room.StopTimer();
                }
            }
            catch (Exception exception)
            {
                if (log.IsErrorEnabled)
                {
                    log.Error("OnTick", exception);
                }
            }
        }
    }
}

