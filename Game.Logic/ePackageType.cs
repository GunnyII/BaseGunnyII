using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Logic
{
    public enum ePackageType
    {
        GAME_CMD = 91,
        GAME_CHAT = 3,
        GAME_ROOM_REMOVEPLAYER = 5,
        GAME_ROOM = 94,
        GAME_MISSION_START = 82,
        //GAME_PLAYER_EXIT = 0x53,
        /// <summary>
        /// 获取全部BUFFER
        /// </summary>
        BUFF_OBTAIN = 168,
    }
}
