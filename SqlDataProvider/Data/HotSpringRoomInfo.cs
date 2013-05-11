using System;
using System.Collections.Generic;
using System.Text;

namespace SqlDataProvider.Data
{
    public class HotSpringRoomInfo
    {
        public int AvailTime { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime BreakTime { get; set; }

        public int MapIndex { get; set; }

        public int MaxCount { get; set; }

        public int PlayerID { get; set; }

        public string PlayerName { get; set; }

        public string Pwd { get; set; }

        public int RoomID { get; set; }

        public string RoomIntroduction { get; set; }

        public string RoomName { get; set; }

        public int RoomNumber { get; set; }

        public int RoomType { get; set; }

        public int ServerID { get; set; }

      }
}
