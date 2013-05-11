using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Logic;
using Game.Base.Packets;

namespace Game.Server.Rooms
{
    class RoomSetupChangeAction : IAction
    {
        private BaseRoom m_room;

        private eRoomType m_roomType;

        private byte m_timeMode;

        private eHardLevel m_hardLevel;

        private int m_mapId;

        private int m_levelLimits;

        private string m_password;

        private string m_roomName;

        private bool m_isCrosszone;

        private bool m_isOpenBoss;

        public RoomSetupChangeAction(BaseRoom room, eRoomType roomType, byte timeMode, eHardLevel hardLevel, int levelLimits, int mapId, string password, string roomname, bool isCrosszone, bool isOpenBoss)
        {
            m_room = room;
            m_roomType = roomType;
            m_timeMode = timeMode;
            m_hardLevel = hardLevel;
            m_levelLimits = levelLimits;
            m_mapId = mapId;
            m_password = password;
            m_roomName = roomname;
            m_isCrosszone = isCrosszone;
            m_isOpenBoss = isOpenBoss;
        }

        public void Execute()
        {
            m_room.RoomType = m_roomType;
            m_room.TimeMode = m_timeMode;
            m_room.HardLevel = m_hardLevel;
            m_room.LevelLimits = m_levelLimits;
            m_room.MapId = m_mapId;
            m_room.Name = m_roomName;
            m_room.Password = m_password;
            m_room.isCrosszone = m_isCrosszone;
            m_room.isOpenBoss = m_isOpenBoss;
            m_room.UpdateRoomGameType();
            m_room.SendRoomSetupChange(m_room);
            RoomMgr.WaitingRoom.SendUpdateRoom(m_room);
        }
    }
}
