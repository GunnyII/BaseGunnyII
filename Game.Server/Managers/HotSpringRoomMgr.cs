using Game.Server.HotSpringRooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Util;
using System.Reflection;
using Game.Server.GameObjects;
using SqlDataProvider.Data;
using Bussiness;

namespace Game.Server.Managers
{
    public class HotSpringRoomMgr
    {
        //private static List<HotSpringRoom> ROOM = new List<HotSpringRoom>();

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected static ReaderWriterLock _locker = new ReaderWriterLock();

        protected static Dictionary<int, HotSpringRoom> _Rooms;

        protected static TankHotSpringLogicProcessor _processor = new TankHotSpringLogicProcessor();
        /*
        private static void Caprom()
        {
            List<HotSpringRoom> list = new List<HotSpringRoom>();
            TankHotSpringLogicProcessor processor = new TankHotSpringLogicProcessor();
            foreach (HotSpringRoomInfo info in GetHotRoomInfo())
            {
                HotSpringRoom item = new HotSpringRoom(info, processor);

                list.Add(item);
            }
            ROOM = list;
        }
         */ 
        public static bool Init()
        {
            //Caprom();
            _Rooms = new Dictionary<int, HotSpringRoom>();
            CheckRoomStatus();
            return true;
        }
        private static void CheckRoomStatus()
        {
            using(PlayerBussiness db = new PlayerBussiness())
            {
                HotSpringRoomInfo[] roomInfos = db.GetHotSpringRoomInfo();

                foreach (HotSpringRoomInfo roomInfo in roomInfos)
                {
                
                    if(roomInfo.ServerID != GameServer.Instance.Configuration.ServerID)
                    {
                        continue;
                    }

                    TimeSpan usedTime = DateTime.Now - roomInfo.BeginTime;
                    int timeLeft = roomInfo.AvailTime * 60 - (int)usedTime.TotalMinutes;

                    if (timeLeft > 0)
                    {
                        //创建房间
                        CreateHotSpringRoomFromDB(roomInfo, timeLeft);
                    }
                    else
                    {                         
                        //do something
                    }

                }
            }
        }
        //public static HotSpringRoom[] GetAllHotRoom()
        //{
        //    if (ROOM.Count == 0)
        //    {
        //        Caprom();
        //    }
        //    return ROOM.ToArray();
        //}
        public static HotSpringRoom[] GetAllHotSpringRoom()
        {
            HotSpringRoom[] list = null;
            _locker.AcquireReaderLock();
            try
            {
                list = new HotSpringRoom[_Rooms.Count];
                _Rooms.Values.CopyTo(list, 0);
            }
            finally
            {
                _locker.ReleaseReaderLock();
            }
            return list == null ? new HotSpringRoom[0] : list;
        }
        //public static HotSpringRoom GetHotRoombyID(int id, string pw, ref string msg)
        //{
        //    return ROOM[id - 1];
        //}
        
        public static HotSpringRoom CreateHotSpringRoomFromDB(HotSpringRoomInfo roomInfo, int timeLeft)
        {
            HotSpringRoom room = null;
            _locker.AcquireWriterLock();
            try
            {
                room = new HotSpringRoom(roomInfo, _processor);
                if (room != null)
                {
                    _Rooms.Add(room.Info.RoomID, room);

                    room.BeginTimer(60 * 1000 * timeLeft);
                    return room;
                }
            }
            finally
            {
                _locker.ReleaseWriterLock();
            }

            return null;
        }

        public static HotSpringRoom GetHotSpringRoombyID(int id, string pwd, ref string msg)
        {
            HotSpringRoom room = null;
            _locker.AcquireReaderLock();
            try
            {
                if (id > 0)
                {
                    if (_Rooms.Keys.Contains(id))
                    {
                        if (_Rooms[id].Info.Pwd != pwd)
                        {
                            msg = "Game.Server.Managers.PWDError";
                        }
                        else
                        {
                            room = _Rooms[id];
                        }
                    }
                }
            }
            finally
            {
                _locker.ReleaseReaderLock();
            }
            return room;
        }
    }
}

