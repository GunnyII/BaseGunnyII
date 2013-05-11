using Bussiness;
using Game.Base.Packets;
using Game.Server.GameObjects;
using Game.Server.Managers;
using Game.Server.Packets;
using log4net;
using SqlDataProvider.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Reflection;

namespace Game.Server.HotSpringRooms
{
    public class HotSpringRoom
    {
        private int _count;
        private List<GamePlayer> _guestsList;
        private IHotSpringProcessor _processor;
        private eRoomState _roomState;
        private static object _syncStop = new object();
        private Timer _timer;
        private Timer _timerForHymeneal;
        private List<int> _userForbid;
        private List<int> _userRemoveList;
        public HotSpringRoomInfo Info;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public HotSpringRoom(HotSpringRoomInfo info, IHotSpringProcessor processor)
        {
            Info = info;
            _processor = processor;
            _guestsList = new List<GamePlayer>();
            _count = 0;
            _roomState = eRoomState.FREE;
            _userForbid = new List<int>();
            _userRemoveList = new List<int>();
        }

        public bool AddPlayer(GamePlayer player)
        {
            lock (_syncStop)
            {
                if ((player.CurrentRoom != null) || player.IsInHotSpringRoom)
                {
                    return false;
                }
                if (_guestsList.Count > Info.MaxCount)
                {
                    player.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("HotSpringRoom.Msg1", new object[0]));
                    return false;
                }
                _count++;
                _guestsList.Add(player);
                player.CurrentHotSpringRoom = this;
                player.HotMap = 1;
                if (player.CurrentRoom != null)
                {
                    player.CurrentRoom.RemovePlayerUnsafe(player);
                }
            }
            return true;
        }

        public void BeginTimer(int interval)
        {
            if (_timer == null)
            {
                _timer = new Timer(new TimerCallback(OnTick), null, interval, interval);
            }
            else
            {
                _timer.Change(interval, interval);
            }
        }

        public void BeginTimerForHymeneal(int interval)
        {
            if (_timerForHymeneal == null)
            {
                _timerForHymeneal = new Timer(new TimerCallback(OnTickForHymeneal), null, interval, interval);
            }
            else
            {
                _timerForHymeneal.Change(interval, interval);
            }
        }

        public bool CheckUserForbid(int userID)
        {
            lock (_syncStop)
            {
                return _userForbid.Contains(userID);
            }
        }

        public GamePlayer[] GetAllPlayers()
        {
            lock (_syncStop)
            {
                return _guestsList.ToArray();
            }
        }

        public GamePlayer GetPlayerByUserID(int userID)
        {
            lock (_syncStop)
            {
                foreach (GamePlayer player in _guestsList)
                {
                    if (player.PlayerCharacter.ID == userID)
                    {
                        return player;
                    }
                }
            }
            return null;
        }

        public void KickAllPlayer()
        {
            GamePlayer[] allPlayers = GetAllPlayers();
            foreach (GamePlayer player in allPlayers)
            {
                RemovePlayer(player);
                player.Out.SendMessage(eMessageType.ChatNormal, LanguageMgr.GetTranslation("HotSpringRoom.TimeOver", new object[0]));
            }
        }

        public bool KickPlayerByUserID(GamePlayer player, int userID)
        {
            GamePlayer playerByUserID = GetPlayerByUserID(userID);
            if ((playerByUserID != null) && 
                //(playerByUserID.PlayerCharacter.ID != player.CurrentHotSpringRoom.Info.GroomID)) && 
                playerByUserID.PlayerCharacter.ID != player.CurrentHotSpringRoom.Info.PlayerID)
            {
                RemovePlayer(playerByUserID);
                playerByUserID.Out.SendMessage(eMessageType.ChatERROR, LanguageMgr.GetTranslation("Game.Server.SceneGames.KickRoom", new object[0]));
                GSPacketIn packet = player.Out.SendMessage(eMessageType.ChatERROR, playerByUserID.PlayerCharacter.NickName + "  " + LanguageMgr.GetTranslation("Game.Server.SceneGames.KickRoom2", new object[0]));
                player.CurrentHotSpringRoom.SendToPlayerExceptSelf(packet, player);
                return true;
            }
            return false;
        }

        protected void OnTick(object obj)
        {
            _processor.OnTick(this);
        }

        protected void OnTickForHymeneal(object obj)
        {
            try
            {
            }
            catch (Exception exception)
            {
                if (log.IsErrorEnabled)
                {
                    log.Error("OnTickForHymeneal", exception);
                }
            }
        }

        public void ProcessData(GamePlayer player, GSPacketIn data)
        {
            lock (_syncStop)
            {
                _processor.OnGameData(this, player, data);
            }
        }

        public void RemovePlayer(GamePlayer player)
        {
            lock (_syncStop)
            {
            }
        }

        public void ReturnPacket(GamePlayer player, GSPacketIn packet)
        {
            GSPacketIn pkg = packet.Clone();
            pkg.ClientID = player.PlayerCharacter.ID;
            SendToPlayerExceptSelf(pkg, player);
        }

        public void ReturnPacketForScene(GamePlayer player, GSPacketIn packet)
        {
            GSPacketIn pkg = packet.Clone();
            pkg.ClientID = player.PlayerCharacter.ID;
            SendToPlayerExceptSelfForScene(pkg, player);
        }

        public void RoomContinuation(int time)
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - Info.BeginTime);
            int num = ((Info.AvailTime* 60) - span.Minutes) + (time * 60);
            Info.AvailTime += time;
            BeginTimer(60000 * num);
        }

        public GSPacketIn SendHotSpringRoomInfoUpdateToScenePlayers(HotSpringRoom room)
        {
            return new GSPacketIn((byte)ePackageType.HOTSPRING_ROOM_ADD_OR_UPDATE);
        }

        public void SendToAll(GSPacketIn packet)
        {
            SendToAll(packet, null, false);
        }

        public void SendToAll(GSPacketIn packet, GamePlayer self, bool isChat)
        {
            GamePlayer[] allPlayers = GetAllPlayers();
            if (allPlayers != null)
            {
                foreach (GamePlayer player in allPlayers)
                {
                    if (!isChat || !player.IsBlackFriend(self.PlayerCharacter.ID))
                    {
                        player.Out.SendTCP(packet);
                    }
                }
            }
        }

        public void SendToAllForScene(GSPacketIn packet, int sceneID)
        {
            GamePlayer[] allPlayers = GetAllPlayers();
            if (allPlayers != null)
            {
                foreach (GamePlayer player in allPlayers)
                {
                    if (player.HotMap == sceneID)
                    {
                        player.Out.SendTCP(packet);
                    }
                }
            }
        }

        public void SendToPlayerExceptSelf(GSPacketIn packet, GamePlayer self)
        {
            GamePlayer[] allPlayers = GetAllPlayers();
            if (allPlayers != null)
            {
                foreach (GamePlayer player in allPlayers)
                {
                    if (player != self)
                    {
                        player.Out.SendTCP(packet);
                    }
                }
            }
        }

        public void SendToPlayerExceptSelfForScene(GSPacketIn packet, GamePlayer self)
        {
            GamePlayer[] allPlayers = GetAllPlayers();
            if (allPlayers != null)
            {
                foreach (GamePlayer player in allPlayers)
                {
                    if ((player != self) && (player.HotMap == self.HotMap))
                    {
                        player.Out.SendTCP(packet);
                    }
                }
            }
        }

        public void SendToRoomPlayer(GSPacketIn packet)
        {
            GamePlayer[] allPlayers = GetAllPlayers();
            if (allPlayers != null)
            {
                foreach (GamePlayer player in allPlayers)
                {
                    player.Out.SendTCP(packet);
                }
            }
        }

        public void SendToScenePlayer(GSPacketIn packet)
        {
            WorldMgr.HotSpring.SendToALL(packet);
        }

        public void SendUserRemoveLate()
        {
            lock (_syncStop)
            {
                foreach (int num in _userRemoveList)
                {
                    GSPacketIn packet = new GSPacketIn(0xf4, num);
                    SendToAllForScene(packet, 1);
                }
                _userRemoveList.Clear();
            }
        }

        public void SetUserForbid(int userID)
        {
            lock (_syncStop)
            {
                _userForbid.Add(userID);
            }
        }

        public void StopTimer()
        {
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
        }

        public void StopTimerForHymeneal()
        {
            if (_timerForHymeneal != null)
            {
                _timerForHymeneal.Dispose();
                _timerForHymeneal = null;
            }
        }

        public int Count
        {
            get
            {
                return _count;
            }
        }

        public eRoomState RoomState
        {
            get
            {
                return _roomState;
            }
            set
            {
                if (_roomState != value)
                {
                    _roomState = value;
                    SendHotSpringRoomInfoUpdateToScenePlayers(this);
                }
            }
        }
    }
}

