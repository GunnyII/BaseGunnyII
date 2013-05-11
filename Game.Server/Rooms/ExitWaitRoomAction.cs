using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Server.GameObjects;

namespace Game.Server.Rooms
{
    public class ExitWaitRoomAction : IAction
    {
        GamePlayer m_player;

        public ExitWaitRoomAction(GamePlayer player)
        {
            m_player = player;
        }

        public void Execute()
        {            

            BaseWaitingRoom room = RoomMgr.WaitingRoom;
            room.RemovePlayer(m_player);
            
        }

    }
}
